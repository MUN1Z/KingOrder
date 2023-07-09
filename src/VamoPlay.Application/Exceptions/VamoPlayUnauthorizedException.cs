using System.Net;

namespace VamoPlay.Application.Exceptions
{
    public class VamoPlayUnauthorizedException : VamoPlayException
    {
        #region constructors

        public VamoPlayUnauthorizedException(string message = "") : base(message, HttpStatusCode.Unauthorized) { }

        #endregion
    }
}
