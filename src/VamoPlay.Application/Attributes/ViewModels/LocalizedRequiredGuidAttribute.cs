using System;
using System.ComponentModel.DataAnnotations;
using VamoPlay.Application.Resources;

namespace VamoPlay.Application.Attributes
{
    public class LocalizedRequiredGuidAttribute : RequiredAttribute
    {
        #region constructors

        public LocalizedRequiredGuidAttribute(string resourceKey)
            : base()
        {
            ErrorMessage = VamoPlayResourceManager.GetInstance().GetMessageFromResource(resourceKey);
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
