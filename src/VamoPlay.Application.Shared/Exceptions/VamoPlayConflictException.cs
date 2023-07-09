using System.Net;

namespace VamoPlay.Application.Shared.Exceptions
{
    public class VamoPlayConflictException : VamoPlayException
    {
        #region constructors

        public VamoPlayConflictException(string message = "", object customData = null) : base(message, HttpStatusCode.Conflict, customData) { }

        #endregion
    }
}
