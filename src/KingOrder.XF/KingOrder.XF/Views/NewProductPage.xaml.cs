using KingOrder.XF.ViewModels;
using Xamarin.Forms;

namespace KingOrder.XF.Views
{
    public partial class NewProductPage : ContentPage
    {
        #region Constructors

        public NewProductPage()
        {
            InitializeComponent();
            BindingContext = new NewProductViewModel();
        }

        #endregion
    }
}