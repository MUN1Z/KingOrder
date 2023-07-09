using System.ComponentModel.DataAnnotations;
using VamoPlay.Application.Shared.Resources;

namespace VamoPlay.Application.Shared.Attributes
{
    public class LocalizedMaxLengthAttribute : MaxLengthAttribute
    {
        #region constructors

        public LocalizedMaxLengthAttribute(int maxLength, string menssageKey) : base(maxLength)
        {
            ErrorMessage = CisopResourceManager.GetInstance().GetMessageFromResource(menssageKey, maxLength.ToString());
        }

        #endregion
    }
}
