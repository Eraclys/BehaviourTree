using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace BehaviourTree.Demo
{
    public static class ImageExtensions
    {
        public static Image Resize(this Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static Image Resize(this Image image, float scale)
        {
            return image.Resize((int)(image.Width * scale), (int)(image.Height * scale));
        }

        public static Image Select(this Image image, Rectangle area)
        {
            var returnBitmap = new Bitmap(area.Width, area.Height);

            using (var graphics = Graphics.FromImage(returnBitmap))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                graphics.DrawImage(
                    image,
                    new Rectangle(0,0, area.Width, area.Height),
                    area.X,
                    area.Y,
                    area.Width,
                    area.Height,
                    GraphicsUnit.Pixel);
            }

            return returnBitmap;
        }
    }
}