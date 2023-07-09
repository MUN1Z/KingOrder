using System.ComponentModel.DataAnnotations;
using VamoPlay.Application.Resources;

namespace VamoPlay.Application.Attributes
{
    public class LocalizedRangeLengthAttribute : RangeAttribute
    {
        #region constructors

        public LocalizedRangeLengthAttribute(int minLength, int maxLength, string menssageKey) : base(minLength, maxLength)
        {
            ErrorMessage = CisopResourceManager.GetInstance().GetMessageFromResource(menssageKey, minLength.ToString(), maxLength.ToString());
        }

        #endregion
    }
}
