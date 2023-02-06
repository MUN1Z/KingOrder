namespace KingOrder.API.IntegrationTests.Factories
{
    public sealed class KingOrderApiWebApplicationFactory : BaseWebApplicationFactory<Startup>
    {
        #region private members

        private static KingOrderApiWebApplicationFactory _instance;

        #endregion

        #region constructors

        private KingOrderApiWebApplicationFactory() { }

        #endregion

        #region public methods implementation

        public static KingOrderApiWebApplicationFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new KingOrderApiWebApplicationFactory();
            }

            return _instance;
        }

        #endregion
    }
}
