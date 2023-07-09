using System.ComponentModel.DataAnnotations;
using VamoPlay.Application.Shared.Resources;

namespace VamoPlay.Application.Shared.Attributes
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
