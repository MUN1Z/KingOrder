using System.ComponentModel.DataAnnotations;
using VamoPlay.Application.Resources;

namespace VamoPlay.Application.Attributes
{
    public class LocalizedMaxLengthAttribute : MaxLengthAttribute
    {
        #region constructors

        public LocalizedMaxLengthAttribute(int maxLength, string menssageKey) : base(maxLength)
        {
            ErrorMessage = VamoPlayResourceManager.GetInstance().GetMessageFromResource(menssageKey, maxLength.ToString());
        }

        #endregion
    }
}
