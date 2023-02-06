using KingOrder.Application.Shared.ViewModels.Response;
using KingOrder.XF.Views;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KingOrder.XF.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        #region Private Members

        private ProductResponseViewModel _selectedProduct;

        #endregion

        #region Properties

        public ObservableCollection<ProductResponseViewModel> Products { get; }

        public ProductResponseViewModel SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                SetProperty(ref _selectedProduct, value);
                OnProductSelectedCommand(value);
            }
        }

        #endregion

        #region Commands

        public Command LoadProductsCommand { get; }
        public Command AddProductCommand { get; }
        public Command<ProductResponseViewModel> ProductTapped { get; }

        #endregion

        #region Constructors
        public ProductsViewModel()
        {
            Title = "Products";
            Products = new ObservableCollection<ProductResponseViewModel>();
            LoadProductsCommand = new Command(async () => await ExecuteLoadProductsCommand());
            ProductTapped = new Command<ProductResponseViewModel>(OnProductSelectedCommand);
            AddProductCommand = new Command(OnAddProductCommand);
        }

        #endregion

        #region Commands Implementations

        async Task ExecuteLoadProductsCommand()
        {
            IsBusy = true;

            try
            {
                Products.Clear();
                var products = await _kingOrderApiService.GetProductsAsync();
                foreach (var prod in products)
                    Products.Add(prod);

                Analytics.TrackEvent("ProductsViewModel Loaded Products");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Crashes.TrackError(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnAddItemCommand(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewProductPage));
        }

        private async void OnAddProductCommand(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewProductPage));
        }

        async void OnProductSelectedCommand(ProductResponseViewModel product)
        {
            if (product == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ProductDetailPage)}?{nameof(ProductDetailViewModel.ProductGuid)}={product.Guid}");
        }

        #endregion

        #region public Methods

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedProduct = null;
        }

        #endregion
    }
}