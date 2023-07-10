using System.ComponentModel.DataAnnotations;
using VamoPlay.Application.Resources;

namespace VamoPlay.Application.Attributes
{
    public class LocalizedEmailAddressAttribute : RegularExpressionAttribute
    {
        #region constants

        private const string _emailRegularExpression = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{1,}";

        #endregion

        #region constructors

        public LocalizedEmailAddressAttribute(string menssageKey) : base(_emailRegularExpression)
        {
            ErrorMessage = VamoPlayResourceManager.GetInstance().GetMessageFromResource(menssageKey);
        }

        #endregion
    }
}
