using System.IO;
using KingOrder.XF.Ioc;
using KingOrder.XF.Ios.Implementations;
using Xamarin.Forms;
using System.Drawing;
using UIKit;
using CoreGraphics;
using System;

[assembly: Dependency(typeof(ImageResizer))]
namespace KingOrder.XF.Ios.Implementations
{
    public class ImageResizer : IImageResizer
    {
        public byte[] ResizeImage(byte[] imageData, float width, float height, int size = 0)
        {
            UIImage originalImage = ImageFromByteArray(imageData);
            UIImageOrientation orientation = originalImage.Orientation;

            if (size != 0)
            {
                width = (float)originalImage.Size.Width / size;
                height = (float)originalImage.Size.Height / size;
            }

            //create a 24bit RGB image
            using (CGBitmapContext context = new CGBitmapContext(IntPtr.Zero,
                                                 (int)width, (int)height, 8,
                                                 4 * (int)width, CGColorSpace.CreateDeviceRGB(),
                                                 CGImageAlphaInfo.PremultipliedFirst))
            {

                RectangleF imageRect = new RectangleF(0, 0, width, height);

                // draw the image
                context.DrawImage(imageRect, originalImage.CGImage);

                UIKit.UIImage resizedImage = UIKit.UIImage.FromImage(context.ToImage(), 0, orientation);

                // save the image as a jpeg
                return resizedImage.AsJPEG().ToArray();
            }
        }

        public static UIKit.UIImage ImageFromByteArray(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            UIKit.UIImage image;
            try
            {
                image = new UIKit.UIImage(Foundation.NSData.FromArray(data));
            }
            catch (Exception e)
            {
                Console.WriteLine("Image load failed: " + e.Message);
                return null;
            }
            return image;
        }
    }
}