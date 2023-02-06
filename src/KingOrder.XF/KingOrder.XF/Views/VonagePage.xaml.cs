using KingOrder.XF.ViewModels;
using Xamarin.Forms;

namespace KingOrder.XF.Views
{
    public partial class VonagePage : ContentPage
    {
        #region Constructors

        public VonagePage()
        {
            InitializeComponent();
            BindingContext = new VonageViewModel();
        }

        #endregion
    }
}