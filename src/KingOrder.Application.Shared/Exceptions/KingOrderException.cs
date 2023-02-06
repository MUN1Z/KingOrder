using System;
using System.Net;

namespace KingOrder.Application.Shared.Exceptions
{
    public class KingOrderException : Exception
    {
        #region properties

        public HttpStatusCode HttpStatusCode { get; }

        public object CustomData { get; }

        #endregion

        #region constructors

        public KingOrderException(string message = "", HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest, object customData = null) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            CustomData = customData;
        }

        #endregion
    }
}
