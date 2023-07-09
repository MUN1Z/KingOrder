using System;
using System.ComponentModel.DataAnnotations;
using VamoPlay.Application.Shared.Attributes;
using VamoPlay.Application.Shared.Constants;
using VamoPlay.Application.Shared.Resources;

namespace VamoPlay.Application.Shared.ViewModels.Response
{
    [Serializable]
    public class LogInRequestViewModel : IViewModel
    {
        [LocalizedRequired(CisopResourceManager.EmailIsRequired)]
        [LocalizedEmailAddress(CisopResourceManager.InvalidEmailFormat)]
        [LocalizedMaxLength(RequestConstants.Length255, CisopResourceManager.EmailMaxLength)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [LocalizedRequired(CisopResourceManager.PasswordIsRequired)]
        [LocalizedMaxLength(RequestConstants.Length255, CisopResourceManager.PasswordMaxLength)]
        public string Password { get; set; }
    }
}
