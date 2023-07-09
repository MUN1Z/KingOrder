using System.Net;

namespace VamoPlay.Application.Exceptions
{
    public class VamoPlayNotFoundException : VamoPlayException
    {
        #region constructors

        public VamoPlayNotFoundException(string message = "") : base(message, HttpStatusCode.NotFound) { }

        #endregion
    }
}
