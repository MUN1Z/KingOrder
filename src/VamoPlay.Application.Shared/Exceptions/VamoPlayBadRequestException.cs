using System.Net;

namespace VamoPlay.Application.Shared.Exceptions
{
    public class VamoPlayBadRequestException : VamoPlayException
    {
        #region constructors

        public VamoPlayBadRequestException(string message = "", object customData = null) : base(message, HttpStatusCode.BadRequest, customData) { }

        #endregion
    }
}
