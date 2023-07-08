using AutoMapper;
using VamoPlay.Application.Comparers;
using VamoPlay.Application.Shared.Exceptions;
using VamoPlay.Application.Filters;
using VamoPlay.Application.Services.Interfaces;
using VamoPlay.Application.Shared.ViewModels.Response;
using VamoPlay.Domain.Entities;
using VamoPlay.Domain.Interfaces;
using VamoPlay.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace VamoPlay.Application.Services
{
    public class ProductService : BaseService, IProductService
    {
        #region private members

        private readonly IProductRepository _productRepository;

        #endregion private members

        #region constructors

        public ProductService(
            IProductRepository ProductRepository,
            IUnitOfWork work,
            IMapper mapper) : base(work, mapper)
        {
            _productRepository = ProductRepository;
        }

        #endregion constructors

        #region public methods implementations

        public async Task<IEnumerable<ProductResponseViewModel>> GetAll(ProductFilter filter)
        {
            filter.Validate();

            var products = new List<ProductResponseViewModel>();

            var (productsDb, totalCountDb) = await _productRepository.FindAllByAsync(filter, hasPagination: false);

            //var productsDb = await _productRepository.GetAllAsync();

            products = _mapper.Map<IEnumerable<ProductResponseViewModel>>(productsDb).ToList();
           
            return products.OrderByDescending(c => c.Favorite, new BooleanComparer()).ThenBy(c => c.Name);
        }

        public async Task<ProductResponseViewModel> GetByGuid(Guid guid)
        {
            var productDb = await _productRepository.GetByIdAsync(guid);

            if (productDb == null)
                throw new VamoPlayException("Product not found!");

            return _mapper.Map<ProductResponseViewModel>(productDb);
        }

        public async Task<ProductResponseViewModel> Create(ProductRequestViewModel productViewModel)
        {
            BeginTransaction();

            var existentProduct = await _productRepository.GetByGtinAsync(productViewModel.Gtin);

            if (existentProduct != null)
                throw new VamoPlayException("Product with same Gtin already registered!");

            var product = _mapper.Map<Product>(productViewModel);

            //generatebarcode
            product.BarCode = "base64here";

            await _productRepository.AddAsync(product);

            Commit();

            return _mapper.Map<ProductResponseViewModel>(product);
        }

        public async Task<ProductResponseViewModel> Favorite(Guid guid)
        {
            var productDb = await _productRepository.GetByIdAsync(guid);

            if (productDb == null)
                throw new VamoPlayException("Product not found!");

            BeginTransaction();

            productDb.Favorite = !productDb.Favorite;
            await _productRepository.UpdateAsync(productDb);

            Commit();

            return _mapper.Map<ProductResponseViewModel>(productDb);
        }

        public async Task<bool> Delete(Guid guid)
        {
            var productDb = await _productRepository.GetByIdAsync(guid);

            if (productDb == null)
                throw new VamoPlayException("Product not found!");

            BeginTransaction();

            await _productRepository.RemoveAsync(productDb);

            Commit();

            return true;
        }

        public async Task<ProductResponseViewModel> Update(Guid guid, ProductRequestViewModel productViewModel)
        {
            var productDb = await _productRepository.GetByIdAsync(guid);

            if (productDb == null)
                throw new VamoPlayException("Product not found!");

            var existentProduct = await _productRepository.FindByAsync(c => c.Gtin == productViewModel.Gtin && c.Guid != guid);

            if (existentProduct != null)
                throw new VamoPlayException("Another product with same Gtin already registered!");

            BeginTransaction();

            productDb.Gtin = productViewModel.Gtin;
            productDb.Name = productViewModel.Name;
            productDb.Description = productViewModel.Description;
            productDb.Price = productViewModel.Price;
            productDb.Discount = productViewModel.Discount;
            productDb.Thumb = productViewModel.Thumb;
            productDb.Thumb = productViewModel.Thumb;

            //generatebarcode
            productDb.BarCode = "base64here";

            await _productRepository.UpdateAsync(productDb);

            Commit();

            return _mapper.Map<ProductResponseViewModel>(productDb);
        }

        #endregion public methods implementations
    }
}
