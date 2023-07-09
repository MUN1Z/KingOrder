using VamoPlay.Application.Extensions;
using VamoPlay.Application.ViewModels.Response;
using VamoPlay.Application.Services.Interfaces;

namespace VamoPlay.Application.Services
{
    public class VamoPlayApiServiceInternal : IVamoPlayApiService
    {
        #region Private Members

        //private readonly string _url = "http://10.0.2.2:5032/api";
        private readonly string _url = "https://kingorderapi.azurewebsites.net/api";
        private readonly HttpClient _httpClient;

        #endregion

        #region Constructors

        public VamoPlayApiServiceInternal()
        {
            _httpClient = new HttpClient();
        }

        #endregion

        #region Public Methods 

        public async Task<IEnumerable<ProductResponseViewModel>> GetProductsAsync()
        {
            var requestUri = _url + "/product";
            return await _httpClient.GetAndDeserialize <IEnumerable<ProductResponseViewModel>> (requestUri);
        }

        public async Task<ProductResponseViewModel> GetProductByGuidAsync(Guid guid)
            => await GetProductByGuidAsync(guid.ToString());

        public async Task<ProductResponseViewModel> GetProductByGuidAsync(string guid)
        {
            var requestUri = _url + $"/product/{guid}";
            return await _httpClient.GetAndDeserialize<ProductResponseViewModel>(requestUri);
        }

        public async Task<ProductResponseViewModel> CreateProduct(ProductRequestViewModel product)
        {
            var requestUri = _url + $"/product";
            return await _httpClient.PostAndDeserialize<ProductResponseViewModel>(requestUri, product);
        }

        public async Task<ProductResponseViewModel> FavoriteProduct(Guid guid)
        {
            var requestUri = _url + $"/product/favorite/{guid}";
            return await _httpClient.PutAndDeserialize<ProductResponseViewModel>(requestUri, null);
        }

        public async Task<ProductResponseViewModel> UpdateProduct(Guid guid, ProductRequestViewModel product)
        {
            var requestUri = _url + $"/product/{guid}";
            return await _httpClient.PutAndDeserialize<ProductResponseViewModel>(requestUri, product);
        }

        public async Task<bool> DeleteProduct(Guid guid)
        {
            var requestUri = _url + $"/product/{guid}";
            return await _httpClient.DeleteAndDeserialize<bool>(requestUri);
        }

        #endregion
    }
}