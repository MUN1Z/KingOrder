using System;
using System.ComponentModel.DataAnnotations;
using VamoPlay.Application.Attributes;
using VamoPlay.Application.Constants;
using VamoPlay.Application.Resources;

namespace VamoPlay.Application.ViewModels.Response
{
    [Serializable]
    public class LogInRequestViewModel : IViewModel
    {
        [LocalizedRequired(VamoPlayResourceManager.EmailIsRequired)]
        [LocalizedEmailAddress(VamoPlayResourceManager.InvalidEmailFormat)]
        [LocalizedMaxLength(RequestConstants.Length255, VamoPlayResourceManager.EmailMaxLength)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [LocalizedRequired(VamoPlayResourceManager.PasswordIsRequired)]
        [LocalizedMaxLength(RequestConstants.Length255, VamoPlayResourceManager.PasswordMaxLength)]
        public string Password { get; set; }
    }
}
