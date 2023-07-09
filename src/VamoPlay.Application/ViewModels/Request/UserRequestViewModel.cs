using System;
using System.ComponentModel.DataAnnotations;
using VamoPlay.Application.Attributes;
using VamoPlay.Application.Constants;
using VamoPlay.Application.Resources;

namespace VamoPlay.Application.ViewModels.Response
{
    [Serializable]
    public class UserRequestViewModel : IViewModel
    {
        [LocalizedRequired(CisopResourceManager.NameIsRequired)]
        [LocalizedMaxLength(RequestConstants.Length255, CisopResourceManager.NameMaxLength)]
        public string Name { get; set; }

        [LocalizedRequired(CisopResourceManager.EmailIsRequired)]
        [LocalizedEmailAddress(CisopResourceManager.InvalidEmailFormat)]
        [LocalizedMaxLength(RequestConstants.Length255, CisopResourceManager.EmailMaxLength)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? LastAccess { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
