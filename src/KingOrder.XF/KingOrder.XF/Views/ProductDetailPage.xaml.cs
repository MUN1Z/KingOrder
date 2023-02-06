using KingOrder.XF.ViewModels;
using Xamarin.Forms;

namespace KingOrder.XF.Views
{
    public partial class ProductDetailPage : ContentPage
    {
        #region Constructors

        public ProductDetailPage()
        {
            InitializeComponent();
            BindingContext = new ProductDetailViewModel();
        }

        #endregion
    }
}