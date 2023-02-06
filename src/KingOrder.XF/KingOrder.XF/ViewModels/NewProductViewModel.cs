using KingOrder.Application.Shared.ViewModels.Response;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using KingOrder.XF.Ioc;
using KingOrder.XF.Extensions;

namespace KingOrder.XF.ViewModels
{
    public class NewProductViewModel : BaseViewModel
    {
        #region Private Members

        private ImageSource _photo;
        private string _gtin;
        private string _name;
        private string _description;
        private decimal _price;
        private decimal _discount;
        private string _thumb;

        #endregion

        #region Properties

        public ImageSource Photo
        {
            get => _photo;
            set => SetProperty(ref _photo, value);
        }

        public string Gtin
        {
            get => _gtin;
            set => SetProperty(ref _gtin, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public decimal Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        public decimal Discount
        {
            get => _discount;
            set => SetProperty(ref _discount, value);
        }

        public string Thumb
        {
            get => _thumb;
            set => SetProperty(ref _thumb, value);
        }

        #endregion

        #region Commands

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command TakePhotoCommand { get; }

        #endregion

        #region Constructors

        public NewProductViewModel()
        {
            Title = "New Product";

            SaveCommand = new Command(OnSaveCommand, ValidateSave);
            CancelCommand = new Command(OnCancelCommand);
            TakePhotoCommand = new Command(OnTakePhotoCommand);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        #endregion

        #region Private Methods

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(_gtin)
                && !String.IsNullOrWhiteSpace(_name)
                && !String.IsNullOrWhiteSpace(_description)
                && !String.IsNullOrWhiteSpace(_price.ToString())
                && !String.IsNullOrWhiteSpace(_discount.ToString())
                && !String.IsNullOrWhiteSpace(_thumb);
        }

        #endregion

        #region Command Implementations

        private async void OnCancelCommand()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSaveCommand()
        {
            try
            {
                var newProduct = new ProductRequestViewModel()
                {
                    Gtin = Gtin,
                    Name = Name,
                    Description = Description,
                    Price = Price,
                    Discount = Discount,
                    Thumb = Thumb,
                };

                IsBusy = true;
                await _kingOrderApiService.CreateProduct(newProduct);
                IsBusy = false;

                await Shell.Current.GoToAsync("..");

                Analytics.TrackEvent("NewProductViewModel Save new Product");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Crashes.TrackError(ex);
            }
        }

        private async void OnTakePhotoCommand()
        {
            try
            {
                var result = await MediaPicker.CapturePhotoAsync();

                await LoadPhotoAsync(result);
            }
            catch (FeatureNotSupportedException fnsEx)
            {

                Debug.WriteLine(fnsEx.Message);
                Crashes.TrackError(fnsEx);
            }
            catch (PermissionException pEx)
            {

                Debug.WriteLine(pEx.Message);
                Crashes.TrackError(pEx);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Crashes.TrackError(ex);
            }
        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled  
            if (photo == null)
            {
                return;
            }

            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            {
                var imageResizerService = DependencyService.Get<IImageResizer>();
                var resizedImage = imageResizerService.ResizeImage(stream.ReadAllBytes(), 150, 150, 10);
                var resizedStream = new MemoryStream(resizedImage);

                var base64 = Convert.ToBase64String(resizedStream.ReadAllBytes());
                Thumb = base64;
                Photo = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(base64)));
            }
        }
        #endregion
    }
}
