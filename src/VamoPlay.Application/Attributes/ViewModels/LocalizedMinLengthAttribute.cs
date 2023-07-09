using System.ComponentModel.DataAnnotations;
using VamoPlay.Application.Resources;

namespace VamoPlay.Application.Attributes
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
