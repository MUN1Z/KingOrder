using System;
using System.ComponentModel.DataAnnotations;
using VamoPlay.Application.Shared.Resources;

namespace VamoPlay.Application.Shared.Attributes
{
    public class LocalizedRequiredGuidAttribute : RequiredAttribute
    {
        #region constructors

        public LocalizedRequiredGuidAttribute(string resourceKey)
            : base()
        {
            ErrorMessage = CisopResourceManager.GetInstance().GetMessageFromResource(resourceKey);
        }

        #endregion

        #region public methods

        public override bool IsValid(object? value)
        {
            if (value is Guid guid && Guid.Empty.Equals(guid))
            {
                return false;
            }
            else
            {
                return base.IsValid(value);
            }
        }

        #endregion
    }
}
