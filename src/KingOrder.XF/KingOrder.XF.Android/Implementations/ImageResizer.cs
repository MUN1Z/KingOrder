using System.IO;
using KingOrder.XF.Ioc;
using KingOrder.XF.Droid.Implementations;
using Xamarin.Forms;
using Android.Graphics;

[assembly: Dependency(typeof(ImageResizer))]
namespace KingOrder.XF.Droid.Implementations
{
    public class ImageResizer : IImageResizer
    {
        public byte[] ResizeImage(byte[] imageData, float width, float height, int size = 0)
        {
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);

            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);

            if (size != 0)
                resizedImage = Bitmap.CreateScaledBitmap(originalImage, originalImage.Width / size, originalImage.Height / size, false);

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                return ms.ToArray();
            }
        }
    }
}