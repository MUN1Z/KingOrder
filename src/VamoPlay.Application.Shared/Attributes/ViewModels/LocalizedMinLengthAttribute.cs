using System.ComponentModel.DataAnnotations;
using VamoPlay.Application.Shared.Resources;

namespace VamoPlay.Application.Shared.Attributes
{
    public class LocalizedMinLengthAttribute : MinLengthAttribute
    {
        #region constructors

        public LocalizedMinLengthAttribute(int minLength, string menssageKey) : base(minLength)
        {
            ErrorMessage = CisopResourceManager.GetInstance().GetMessageFromResource(menssageKey, minLength.ToString());
        }

        #endregion
    }
}
