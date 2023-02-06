using KingOrder.Application.Shared.ViewModels.Response;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace KingOrder.XF.ViewModels
{
    [QueryProperty(nameof(ProductGuid), nameof(ProductGuid))]
    public class ProductDetailViewModel : BaseViewModel
    {
        #region Private Members

        private ProductResponseViewModel _product;
        private string _productGuid;

        #endregion

        #region Properties

        public string ProductGuid
        {
            get
            {
                return _productGuid;
            }
            set
            {
                _productGuid = value;
                LoadProductByGuid(value);
            }
        }

        public ProductResponseViewModel Product
        {

            get => _product;
            set => SetProperty(ref _product, value);
        }

        #endregion

        #region Commands

        public Command FavoriteToggleCommand { get; }
        public Command EditCommand { get; }
        public Command DeleteCommand { get; }

        #endregion

        #region Constructors

        public ProductDetailViewModel()
        {
            Title = "Product Details";
            FavoriteToggleCommand = new Command(OnFavoriteToggleCommand);
            EditCommand = new Command(OnEditCommand);
            DeleteCommand = new Command(OnDeleteCommand);
        }

        #endregion

        #region Commands Implementations

        private async void OnFavoriteToggleCommand()
        {
            try
            {
                Analytics.TrackEvent("ProductDetailViewModel OnFavoriteToggleCommand");

                var product = await _kingOrderApiService.GetProductByGuidAsync(Product.Guid);

                if (_product.Favorite == product.Favorite)
                    return;

                IsBusy = true;
                await _kingOrderApiService.FavoriteProduct(Product.Guid);
                IsBusy = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Crashes.TrackError(ex);
            }
        }

        private async void OnDeleteCommand()
        {
            try
            {
                Analytics.TrackEvent("ProductDetailViewModel OnDeleteCommand");
                IsBusy = true;
                await _kingOrderApiService.DeleteProduct(Product.Guid);
                IsBusy = false;

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Crashes.TrackError(ex);
            }
        }

        private async void OnEditCommand()
        {
            try
            {
                Analytics.TrackEvent("ProductDetailViewModel OnEditCommand");
                //TODO REDIRECT TO EDIT PAGE
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Crashes.TrackError(ex);
            }
        }

        #endregion

        #region Private Methods

        public async void LoadProductByGuid(string productGuid)
        {
            try
            {
                Analytics.TrackEvent("ProductDetailViewModel Loaded Product Detail");
                IsBusy = true;
                Product = await _kingOrderApiService.GetProductByGuidAsync(productGuid);
                IsBusy = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Crashes.TrackError(ex);
            }
        }

        #endregion
    }
}
