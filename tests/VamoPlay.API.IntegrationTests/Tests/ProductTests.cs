using FluentAssertions;
using VamoPlay.Application.Filters;
using VamoPlay.Application.Extensions;
using VamoPlay.Application.ViewModels.Response;
using VamoPlay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace VamoPlay.API.IntegrationTests.Tests
{
    public class ProductTests : BaseIntegrationTests
    {
        #region Constructors

        public ProductTests() { }

        #endregion

        #region Get Tests

        [Fact(DisplayName = "Get All Products")]
        public async Task Get_All_Products()
        {
            // Arrange
            var productsCount = await _kingOrderContext.Product.CountAsync();
            var filter = new ProductFilter();

            // Act
            var response =
                await _kingOrderHttpClient.GetAndDeserialize<IEnumerable<ProductResponseViewModel>>(filter.ToQueryString($"Product/"));

            //Assert
            response.Count().Should().Be(productsCount);
        }

        [Fact(DisplayName = "Get Product By Guid")]
        public async Task Get_Product_By_Guid()
        {
            // Arrange
            var product = await CreateProduct();

            // Act
            var response =
                await _kingOrderHttpClient.GetAndDeserialize<ProductResponseViewModel>($"api/Product/{product.Guid}");

            //Assert
            response.Name.Should().Be(product.Name);
        }

        #endregion

        #region Put Tests

        [Fact(DisplayName = "Create Product")]
        public async Task Create_Product()
        {
            // Arrange
            var product = new ProductRequestViewModel
            {
                Gtin = GenerateRandomString(13, true),
                Name = GenerateRandomString(10),
                Description = GenerateRandomString(10),
                Price = 10,
                Discount = 1,
                Thumb = GenerateRandomString(255)
            };

            // Act
            var response =
                await _kingOrderHttpClient.PostAndDeserialize<ProductResponseViewModel>("api/Product", product);

            var productDb = await _kingOrderContext.Product.FirstOrDefaultAsync(c => c.Guid == response.Guid);

            //Assert
            response.Name.Should().Be(productDb.Name);
        }

        #endregion

        #region Put Tests

        [Fact(DisplayName = "Update Product")]
        public async Task Update_Product()
        {
            // Arrange
            var product = new ProductRequestViewModel
            {
                Gtin = GenerateRandomString(13, true),
                Name = GenerateRandomString(10),
                Description = GenerateRandomString(10),
                Price = 10,
                Discount = 1,
                Thumb = GenerateRandomString(255)
            };

            // Act
            var responseCreate =
                await _kingOrderHttpClient.PostAndDeserialize<ProductResponseViewModel>("api/Product", product);

            product.Name += " - Updated";
            product.Description = GenerateRandomString(10);
            product.Price = 100;
            product.Discount = 10;

            var responseUpdate =
                await _kingOrderHttpClient.PutAndDeserialize<ProductResponseViewModel>($"api/Product/{responseCreate.Guid}", product);

            var productDb = await _kingOrderContext.Product.FirstOrDefaultAsync(c => c.Guid == responseCreate.Guid);

            //Assert
            responseUpdate.Name.Should().Be(productDb.Name);
            responseUpdate.Description.Should().Be(productDb.Description);
            responseUpdate.Price.Should().Be(productDb.Price);
            responseUpdate.Discount.Should().Be(productDb.Discount);
        }

        [Fact(DisplayName = "Favorite Product")]
        public async Task Favorite_Product()
        {
            // Arrange
            var product = new ProductRequestViewModel
            {
                Gtin = GenerateRandomString(13, true),
                Name = GenerateRandomString(10),
                Description = GenerateRandomString(10),
                Price = 10,
                Discount = 1,
                Thumb = GenerateRandomString(255)
            };

            // Act
            var responseCreate =
                await _kingOrderHttpClient.PostAndDeserialize<ProductResponseViewModel>("api/Product", product);

            var responseFavorite =
                await _kingOrderHttpClient.PutAndDeserialize<ProductResponseViewModel>($"api/Product/favorite/{responseCreate.Guid}", product);

            var productDb = await _kingOrderContext.Product.FirstOrDefaultAsync(c => c.Guid == responseCreate.Guid);

            //Assert
            responseFavorite.Favorite.Should().Be(true);
            productDb.Favorite.Should().Be(true);
            responseFavorite.Favorite.Should().Be(productDb.Favorite);
        }

        #endregion

        #region Delete Tests

        [Fact(DisplayName = "Delete Product")]
        public async Task Delete_Product()
        {
            // Arrange
            var product = new ProductRequestViewModel
            {
                Gtin = GenerateRandomString(13, true),
                Name = GenerateRandomString(10),
                Description = GenerateRandomString(10),
                Price = 10,
                Discount = 1,
                Thumb = GenerateRandomString(255)
            };

            // Act
            var responseCreate =
                await _kingOrderHttpClient.PostAndDeserialize<ProductResponseViewModel>("api/Product", product);

            var responseDelete =
                await _kingOrderHttpClient.DeleteAndDeserialize<bool>($"api/Product/{responseCreate.Guid}");

            var productDb = await _kingOrderContext.Product.FirstOrDefaultAsync(c => c.Guid == responseCreate.Guid);

            //Assert
            responseDelete.Should().Be(true);
            productDb.Should().BeNull();
        }

        #endregion
    }
}
