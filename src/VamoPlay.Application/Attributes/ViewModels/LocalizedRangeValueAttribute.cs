using System.ComponentModel.DataAnnotations;
using VamoPlay.Application.Resources;

namespace VamoPlay.Application.Attributes
{
    public class LocalizedRangeValueAttribute : RangeAttribute
    {
        #region constructors

        public LocalizedRangeValueAttribute(double min, double max, string messageKey)
            : base(min, max)
        {
            ErrorMessage = VamoPlayResourceManager.GetInstance().GetMessageFromResource(messageKey, min.ToString(), max.ToString());
        }

        #endregion
    }
}