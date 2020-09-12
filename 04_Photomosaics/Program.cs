using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _04_Photomosaics
{
    class Program
    {
        static void Main(string[] args)
        {
            const string datasetPath = @"../../images/";
            const int scale = 5;
            const int samplingSizePx = 4;
            var imageMap = BuildImageMapFromDataset(datasetPath, new Size(scale * (samplingSizePx), scale * (samplingSizePx)));

            Image image = Image.FromFile(@"ascii-pineapple.jpg");
            Bitmap newImage = new Bitmap(image.Width * scale, image.Height * scale);

            using (Bitmap bmp = new Bitmap(image))
            {
                for (int i = 0; i < bmp.Height; i += samplingSizePx)
                {
                    for (int j = 0; j < bmp.Width; j += samplingSizePx)
                    {
                        Color average = GetAverageColor(bmp, new Rectangle(j, i, samplingSizePx, samplingSizePx));
                        Bitmap closestImage = FindClosestImageByColor(imageMap, average);
                        using (Graphics g = Graphics.FromImage(newImage))
                        {
                            g.DrawImage(closestImage, new Point(j * scale, i * scale));
                        }
                    }
                }
            }
            newImage.Save("new2.jpg");

        }

        private static Bitmap FindClosestImageByColor(IDictionary<Color, Bitmap> imageMap, Color average)
        {
            if (imageMap.ContainsKey(average))
            {
                return imageMap[average];
            }
            else
            {
                int difference = int.MaxValue;
                //double difference = double.MaxValue;
                Bitmap closest = null;

                foreach (var item in imageMap)
                {
                    int currentDifference = Math.Abs(item.Key.R - average.R)
                        + Math.Abs(item.Key.G - average.G)
                        + Math.Abs(item.Key.B - average.B);

                    //double currentDifference = Math.Sqrt(Math.Pow(item.Key.R - average.R, 2)
                    //    + Math.Pow(item.Key.G - average.G, 2)
                    //    + Math.Pow(item.Key.B - average.B, 2));

                    if (currentDifference < difference)
                    {
                        difference = currentDifference;
                        closest = item.Value;
                    }
                }

                return closest;
            }
        }

        static IDictionary<Color, Bitmap> BuildImageMapFromDataset(string path, Size size)
        {
            IDictionary<Color, Bitmap> imageMap = new Dictionary<Color, Bitmap>();
            foreach (string file in Directory.GetFiles(path))
            {
                Bitmap bmp = new Bitmap(Image.FromFile(file), size);
                Color average = GetAverageColor(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                if (!imageMap.ContainsKey(average))
                {
                    imageMap.Add(average, bmp);
                }
            }
            return imageMap;
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
