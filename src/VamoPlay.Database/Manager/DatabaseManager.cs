using VamoPlay.Database.Contexts;
using VamoPlay.Domain.Entities;
using VamoPlay.Domain.Interfaces;

namespace VamoPlay.Database.Seed
{
    public class DatabaseManager : IDatabaseManager
    {
        #region private members

        private VamoPlayContext _context;

        #endregion

        #region constructors

        public DatabaseManager(VamoPlayContext context)
        {
            context.Database.EnsureCreated();
            _context = context;
        }

        #endregion

        #region public methods implementations

        public async Task SeedData()
        {
            if (!_context.Product.Any())
            {
                var products = new List<Product>();

                for (int i = 0; i < 10; i++)
                {
                    var product = new Product
                    {
                        Guid = Guid.NewGuid(),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        Gtin = GenerateGTIN(),
                        Name = $"ProductName {i}",
                        Description = $"ProductDescription {i}",
                        BarCode = "base64here",
                        Thumb = "base64here"
                    };

                    products.Add(product);
                }

                _context.AddRange(products.DistinctBy(c => c.Gtin));
                _context.SaveChanges();
            }
        }

        #endregion

        #region private methods implementarions

        private string GenerateGTIN()
            => GenerateRandomString(13, true);

        private string GenerateRandomString(int length, bool onlyNumbers = false, bool onlyLetters = false)
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

        #endregion
    }
}
