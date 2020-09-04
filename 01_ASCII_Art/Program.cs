using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Schema;

namespace _01_ASCII_Art
{
    delegate double ColorBightness(Color color);

    class Program
    {
        static void Main(string[] args)
        {
            const bool shouldInvertBrightness = false;
            const bool shouldChangeColor = true;


            Image image = Image.FromFile(@"..\..\ascii-pineapple.jpg");
            //Image image = Image.FromFile(@"..\..\zebra.jpg");
            Size maxSize = new Size(210, 150);

            ColorBightness colorBightness = new ColorBightness(Lightness);

            double MAX_BRIGHTNESS = colorBightness(Color.FromArgb(255, 255, 255));
            string asciiText = "`^\",:;Il!i~+_-?][}{1)(|\\/tfjrxnuvczXYUJCLQ0OZmwqpdbkhao*#MW&8%B@$";
            double ASCII_LENGTH_BRIGHTNESS_FACTOR = (asciiText.Length - 1) / MAX_BRIGHTNESS;
            char[] ascii = asciiText.ToCharArray();

            using (Bitmap bmp = new Bitmap(image, maxSize))
            {
                Console.WriteLine("Width : " + bmp.Width);
                Console.WriteLine("Height : " + bmp.Height);
                for (int i = 0; i < bmp.Height; i++)
                {
                    for (int j = 0; j < bmp.Width; j++)
                    {
                        Color color = bmp.GetPixel(j, i);

                        int index;
                        if (shouldInvertBrightness)
                        {
                            index = (int)(InvertBrightness(colorBightness(color), MAX_BRIGHTNESS) * ASCII_LENGTH_BRIGHTNESS_FACTOR);
                        }
                        else
                        {
                            index = (int)(colorBightness(color) * ASCII_LENGTH_BRIGHTNESS_FACTOR);
                        }

                        if (shouldChangeColor)
                        {
                            float hue = color.GetHue();
                            if (color.GetBrightness() > .7)
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else if (color.GetBrightness() < .3)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                            }
                            else if (hue < 15 || hue >= 345)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                            else if (hue < 75)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                            }
                            else if (hue < 140)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                            else if (hue < 200)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                            }
                            else if (hue < 260)
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                            }
                            else if (hue < 330)
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                            }
                            else if (hue < 345)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }

                        Console.Write(ascii[index] + "" + ascii[index] + "" + ascii[index]);
                    }
                    Console.WriteLine();
                }
            }

            Console.ReadKey();
        }

        static double Average(Color color)
        {
            return (color.R + color.G + color.B) / 3;
        }

        static double Lightness(Color color)
        {
            return Math.Max(Math.Max(color.R, color.G), color.B) +
                Math.Min(Math.Min(color.R, color.G), color.B) / 2;
        }

        static double Luminosity(Color color)
        {
            return 0.21 * color.R + 0.72 * color.G + 0.07 * color.B;
        }

        static double InvertBrightness(double brithness, double maxBrightness)
        {
            return maxBrightness - brithness;
        }
    }
}
