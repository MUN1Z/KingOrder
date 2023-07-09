using System.ComponentModel.DataAnnotations;
using VamoPlay.Application.Resources;

namespace VamoPlay.Application.Attributes
{
    public class LocalizedByteMaxSizeAttribute : MaxLengthAttribute
    {
        #region private members

        readonly int _maxLength;

        const int _byteSize = 1024;

        #endregion

        #region constructors

        public LocalizedByteMaxSizeAttribute(int maxLength, string menssageKey) : base (maxLength * _byteSize * _byteSize)
        {
            _maxLength = maxLength * _byteSize * _byteSize;
            ErrorMessage = CisopResourceManager.GetInstance().GetMessageFromResource(menssageKey, maxLength.ToString());
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            if (value is byte[] == false)
                return false;

            return ((byte[])value).Length <= _maxLength;
        }

        #endregion
    }
}
