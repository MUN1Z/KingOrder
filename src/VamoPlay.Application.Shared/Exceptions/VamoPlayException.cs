using System;
using System.Net;

namespace VamoPlay.Application.Shared.Exceptions
{
    public class VamoPlayException : Exception
    {
        #region properties

        public HttpStatusCode HttpStatusCode { get; }

        public object CustomData { get; }

        #endregion

        #region constructors

        public VamoPlayException(string message = "", HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest, object customData = null) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            CustomData = customData;
        }

        #endregion
    }
}
