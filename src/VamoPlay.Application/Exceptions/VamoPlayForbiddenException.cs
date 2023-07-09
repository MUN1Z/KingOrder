using System.Net;

namespace VamoPlay.Application.Exceptions
{
    public class VamoPlayForbiddenException : VamoPlayException
    {
        #region constructors

        public VamoPlayForbiddenException(string message = "") : base(message, HttpStatusCode.Forbidden) { }

        #endregion
    }
}
