using System.Threading.Tasks;

namespace KingOrder.XF.Ioc
{
    public interface IImageResizer
    {
        byte[] ResizeImage(byte[] imageData, float width, float height, int size = 0);
    }
}
