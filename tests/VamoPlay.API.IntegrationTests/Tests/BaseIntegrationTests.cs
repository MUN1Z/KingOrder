using VamoPlay.API.IntegrationTests.Factories;
using VamoPlay.Database.Contexts;
using VamoPlay.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace VamoPlay.API.IntegrationTests.Tests
{
    public class BaseIntegrationTests
    {
        #region Protected Members

        protected readonly VamoPlayContext _kingOrderContext;
        protected readonly VamoPlayApiWebApplicationFactory _kingOrderFactory;
        protected readonly HttpClient _kingOrderHttpClient;
        protected static object _lock = new object();

        #endregion

        #region Constructors

        public BaseIntegrationTests()
        {
            lock (_lock)
            {
                _kingOrderFactory = VamoPlayApiWebApplicationFactory.GetInstance();
                _kingOrderHttpClient = _kingOrderFactory.CreateClient();
                var serviceScope = _kingOrderFactory.Services.GetService<IServiceScopeFactory>().CreateScope();
                _kingOrderContext = serviceScope?.ServiceProvider.GetService<VamoPlayContext>();
            }
        }

        #endregion

        #region Protected Methods

        protected string GenerateRandomString(int length, bool onlyNumbers = false, bool onlyLetters = false)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            if (onlyNumbers)
                chars = "0123456789";
            else if (onlyLetters)
                chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        protected async Task<Product> CreateProduct()
        {
            var product = new Product
            {
                Guid = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Gtin = GenerateRandomString(13, true),
                Name = GenerateRandomString(10),
                Description = GenerateRandomString(10),
                BarCode = GenerateRandomString(255),
                Thumb = GenerateRandomString(255)
            };

            await _kingOrderContext.Product.AddAsync(product);
            await _kingOrderContext.SaveChangesAsync();

            return product;
        }

        #endregion
    }
}