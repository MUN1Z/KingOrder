using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;

namespace KingOrder.XF.UITests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void ProductsTest()
        {
            //Arrange
            var productsPageName = "Products";

            //Act
            var results = app.WaitForElement(c => c.Marked(productsPageName));
            app.Screenshot("Products screen.");

            //Assert
            Assert.IsTrue(results.Any());
        }

        [Test]
        public void ProductDetailsTest()
        {
            // Arrange
            var firstProductinList = "ProductName 0";
            var productDetailsPageName = "Product Details";

            //Act
            app.WaitForElement(c => c.Marked(firstProductinList));
            app.Screenshot("Products screen.");

            app.Tap(c => c.Text(firstProductinList));

            var resultsInProductDetails = app.WaitForElement(c => c.Marked(productDetailsPageName));

            //Assert
            Assert.IsTrue(resultsInProductDetails.Any());
        }

        [Test]
        public void DeleteProduct()
        {
            // Arrange
            var productList = "ProductName";
            var firstProductinList = "ProductName 1";
            var productDetailsPageName = "Product Details";
            var favorite = "ProductFavoriteToogle";
            var delete = "Delete";

            //Act
            app.WaitForElement(c => c.Marked(firstProductinList));
            app.Screenshot("Products screen.");

            app.Tap(c => c.Text(firstProductinList));

            app.WaitForElement(c => c.Marked(productDetailsPageName));

            //var aa = app.WaitForElement(c => c.Marked("Gtin"));

            app.Tap(c => c.Text(delete));

            var results = app.Query(c => c.Text(firstProductinList));

            //Assert
            Assert.False(results.Any());
        }

        [Test]
        public void SaveProduct()
        {
            //TODO CREATE TEST
            // Arrange
            //var productList = "ProductName";
            //var firstProductinList = "ProductName 1";
            //var productDetailsPageName = "Product Details";
            //var favorite = "ProductFavoriteToogle";
            //var delete = "Delete";

            ////Act
            //app.WaitForElement(c => c.Marked(firstProductinList));
            //app.Screenshot("Products screen.");

            //app.Tap(c => c.Text(firstProductinList));

            //app.WaitForElement(c => c.Marked(productDetailsPageName));

            ////var aa = app.WaitForElement(c => c.Marked("Gtin"));

            //app.Tap(c => c.Text(delete));

            //var results = app.Query(c => c.Text(firstProductinList));

            ////Assert
            //Assert.False(results.Any());
        }

    }
}
