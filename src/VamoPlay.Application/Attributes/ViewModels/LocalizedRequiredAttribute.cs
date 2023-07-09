using System.ComponentModel.DataAnnotations;
using VamoPlay.Application.Resources;

namespace VamoPlay.Application.Attributes
{
    public class LocalizedRequiredAttribute : RequiredAttribute
    {
        #region constructors

        public LocalizedRequiredAttribute(string resourceKey)
            : base()
        {
            ErrorMessage = CisopResourceManager.GetInstance().GetMessageFromResource(resourceKey);
        }

        #endregion
    }
}
