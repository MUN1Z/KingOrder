using System.ComponentModel.DataAnnotations;
using VamoPlay.Application.Shared.Resources;

namespace VamoPlay.Application.Shared.Attributes
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
