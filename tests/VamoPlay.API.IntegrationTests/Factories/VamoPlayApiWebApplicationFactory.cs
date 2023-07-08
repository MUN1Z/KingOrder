namespace VamoPlay.API.IntegrationTests.Factories
{
    public sealed class VamoPlayApiWebApplicationFactory : BaseWebApplicationFactory<Startup>
    {
        #region private members

        private static VamoPlayApiWebApplicationFactory _instance;

        #endregion

        #region constructors

        private VamoPlayApiWebApplicationFactory() { }

        #endregion

        #region public methods implementation

        public static VamoPlayApiWebApplicationFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new VamoPlayApiWebApplicationFactory();
            }

            return _instance;
        }

        #endregion
    }
}
