using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OpenTKExtension
{
    public class Texture
    {
        public byte[] PixelData { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Texture(string path)
        {
            var image = Image.Load<Rgba32>(path);
            image.Mutate(x => x.Flip(FlipMode.Vertical));
            var pixels = new List<byte>(4 * image.Width * image.Height);
            //for (int y = 0; y < image.Height; y++)
            //{
            //    pixels.Add(image[1,0].)
            //    var row = image.GetPixelRowSpan(y);

            //    for (int x = 0; x < image.Width; x++)
            //    {
            //        pixels.Add(row[x].R);
            //        pixels.Add(row[x].G);
            //        pixels.Add(row[x].B);
            //        pixels.Add(row[x].A);
            //    }
            //}

            //int[] pixelData = new int[image.Width * image.Height * 4];

            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    pixels.Add(image[j, i].R);
                    pixels.Add(image[j, i].G);
                    pixels.Add(image[j, i].B);
                    pixels.Add(image[j, i].A);
                }
            }
            Width = image.Width;
            Height = image.Height;
            PixelData = pixels.ToArray();
        }
    }
}
