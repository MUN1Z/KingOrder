using KingOrder.XF.ViewModels;
using Xamarin.Forms;

namespace KingOrder.XF.Views
{
    public partial class ProductsPage : ContentPage
    {
        #region Private Members

        ProductsViewModel _viewModel;

        #endregion

        #region Constructors

        public ProductsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ProductsViewModel();
        }

        #endregion

        #region Protected Methods

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        #endregion
    }
}