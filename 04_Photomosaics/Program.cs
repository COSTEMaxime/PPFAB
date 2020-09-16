using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _04_Photomosaics
{
    class Program
    {
        static readonly char CACHE_SEPARATOR = ';';
        static IDictionary<string, Bitmap> imageCache = new Dictionary<string, Bitmap>();
        static readonly int scale = 5;
        static readonly int samplingSizePx = 4;
        static Size size = new Size(scale* (samplingSizePx), scale* (samplingSizePx));

        static void Main(string[] args)
        {
            const string datasetPath = @"../../images/";
            const string cacheFile = @"../../cache.txt";


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var imageMap = BuildImageMapFromDataset(datasetPath, cacheFile);

            Image image = Image.FromFile(@"ascii-pineapple.jpg");
            Bitmap newImage = new Bitmap(image.Width * scale, image.Height * scale);

            using (Bitmap bmp = new Bitmap(image))
            {
                using (Graphics g = Graphics.FromImage(newImage))
                {
                    for (int i = 0; i < bmp.Height; i += samplingSizePx)
                    {
                        for (int j = 0; j < bmp.Width; j += samplingSizePx)
                        {
                            Color average = GetAverageColor(bmp, new Rectangle(j, i, samplingSizePx, samplingSizePx));
                            Bitmap closestImage = FindClosestImageByColor(imageMap, average);
                            g.DrawImage(closestImage, new Point(j * scale, i * scale));
                        }
                    }
                }
            }
            newImage.Save("new2.jpg");

            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }

        private static Bitmap FindClosestImageByColor(IDictionary<Color, List<Tuple<Color, string>>> imageMap, Color average)
        {
            Color cacheKey = RoundToColor(average);
            List<Tuple<Color, string>> entry = null;
            int difference = int.MaxValue;

            if (imageMap.ContainsKey(cacheKey))
            {
                entry = imageMap[cacheKey];
            }
            else
            {
                foreach (var item in imageMap)
                {
                    int currentDifference = Math.Abs(item.Key.R - average.R)
                        + Math.Abs(item.Key.G - average.G)
                        + Math.Abs(item.Key.B - average.B);

                    if (currentDifference < difference)
                    {
                        difference = currentDifference;
                        entry = item.Value;
                    }
                }
            }

            //double difference = double.MaxValue;
            difference = int.MaxValue;
            string closest = null;

            foreach (var item in entry)
            {
                int currentDifference = Math.Abs(item.Item1.R - average.R)
                    + Math.Abs(item.Item1.G - average.G)
                    + Math.Abs(item.Item1.B - average.B);

                //double currentDifference = Math.Sqrt(Math.Pow(item.Key.R - average.R, 2)
                //    + Math.Pow(item.Key.G - average.G, 2)
                //    + Math.Pow(item.Key.B - average.B, 2));

                if (currentDifference < difference)
                {
                    difference = currentDifference;
                    closest = item.Item2;
                }
            }

            if (!imageCache.ContainsKey(closest))
            {
                Bitmap bmp = new Bitmap(Image.FromFile(closest), size);
                imageCache.Add(closest, bmp);
            }

            return imageCache[closest];
        }

        static IDictionary<Color, List<Tuple<Color, string>>> BuildImageMapFromDataset(string datasetPath, string cachePath = "")
        {
            IDictionary<Color, List<Tuple<Color, string>>> imageMap = new Dictionary<Color, List<Tuple<Color, string>>>();
            IDictionary<string, Color> existingCache = LoadColorCacheFromFile(cachePath);

            foreach (string file in Directory.GetFiles(datasetPath))
            {
                Color average;
                if (!existingCache.ContainsKey(file))
                {
                    Bitmap bmp = new Bitmap(Image.FromFile(file), size);
                    average = GetAverageColor(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));

                    using (StreamWriter sw = File.AppendText(cachePath))
                    {
                        sw.WriteLine(average.R.ToString() + CACHE_SEPARATOR + average.G + CACHE_SEPARATOR + average.B + CACHE_SEPARATOR + file);
                    }

                    imageCache.Add(file, bmp);
                }
                else
                {
                    average = existingCache[file];
                }

                Color cacheKey = RoundToColor(average);

                if (!imageMap.ContainsKey(cacheKey))
                {
                    imageMap.Add(cacheKey, new List<Tuple<Color, string>>());
                }

                imageMap[cacheKey].Add(new Tuple<Color, string>(average, file));
            }
            return imageMap;
        }

        private static IDictionary<string, Color> LoadColorCacheFromFile(string cachePath)
        {
            // Disclaimer : not very effective because the average color calculation is very fast
            // a better approach will be to lazy load the bitmaps when we need them


            IDictionary<string, Color> cache = new Dictionary<string, Color>();
            if (!File.Exists(cachePath))
            {
                File.Create(cachePath);
            }

            foreach (string line in File.ReadAllLines(cachePath))
            {
                string[] tokens = line.Split(CACHE_SEPARATOR);
                cache.Add(tokens[3], Color.FromArgb(int.Parse(tokens[0]), int.Parse(tokens[1]), int.Parse(tokens[2])));
            }

            return cache;
        }

        private static Color RoundToColor(Color average)
        {
            return Color.FromArgb(
                (int)Math.Round(average.R / 10.0) * 10,
                (int)Math.Round(average.R / 10.0) *10,
                (int)Math.Round(average.R / 10.0) *10
            );
        }

        private static Color GetAverageColor(Bitmap bmp, Rectangle zone)
        {
            BitmapData srcData = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb
            );

            int width = zone.Right > bmp.Width ? bmp.Width : zone.Right;
            int height = zone.Bottom > bmp.Height ? bmp.Height : zone.Bottom;
            int size = (zone.X - width) * (zone.Y - height);

            int stride = srcData.Stride;
            IntPtr Scan0 = srcData.Scan0;

            long[] totals = new long[] { 0, 0, 0 };

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                for (int y = zone.Y; y < height; y++)
                {
                    for (int x = zone.X; x < width; x++)
                    {
                        int whiteCount = 0;
                        for (int color = 0; color < 3; color++)
                        {
                            int idx = (y * stride) + x * 4 + color;
                            if (p[idx] == 255)
                            {
                                whiteCount++;
                            }

                            totals[color] += p[idx];
                        }

                        if (whiteCount == 3)
                        {
                            totals[0] -= 255;
                            totals[1] -= 255;
                            totals[2] -= 255;

                            size--;
                        }
                    }
                }
            }
            bmp.UnlockBits(srcData);

            return Color.FromArgb((int)(totals[2] / size), (int)(totals[1] / size), (int)(totals[0] / size));
        }
    }
}
