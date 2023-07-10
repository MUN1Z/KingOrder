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
        [LocalizedRequired(VamoPlayResourceManager.NameIsRequired)]
        [LocalizedMaxLength(RequestConstants.Length255, VamoPlayResourceManager.NameMaxLength)]
        public string Name { get; set; }

        [LocalizedRequired(VamoPlayResourceManager.EmailIsRequired)]
        [LocalizedEmailAddress(VamoPlayResourceManager.InvalidEmailFormat)]
        [LocalizedMaxLength(RequestConstants.Length255, VamoPlayResourceManager.EmailMaxLength)]
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
