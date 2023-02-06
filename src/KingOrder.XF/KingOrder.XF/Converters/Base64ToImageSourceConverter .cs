using KingOrder.XF.Extensions;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;

namespace KingOrder.XF.Converters
{
    public class Base64ToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var base64Value = (string)value;

            if (!string.IsNullOrEmpty(base64Value) && base64Value.IsBase64String())
                return ImageSource.FromStream(() => new MemoryStream(System.Convert.FromBase64String(base64Value)));
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var imageSource = (ImageSource)value;
            var stream = ConvertImageSourceToBytesAsync(imageSource).Result;
            return System.Convert.ToBase64String(stream);
        }

        public async Task<byte[]> ConvertImageSourceToBytesAsync(ImageSource imageSource)
        {
            Stream stream = await ((StreamImageSource)imageSource).Stream(CancellationToken.None);
            byte[] bytesAvailable = new byte[stream.Length];
            stream.Read(bytesAvailable, 0, bytesAvailable.Length);

            return bytesAvailable;
        }
    }
}
