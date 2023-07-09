using System;
using System.ComponentModel.DataAnnotations;
using VamoPlay.Application.Shared.Attributes;
using VamoPlay.Application.Shared.Constants;
using VamoPlay.Application.Shared.Resources;

namespace VamoPlay.Application.Shared.ViewModels.Response
{
    [Serializable]
    public class UserAccountRequestViewModel : IViewModel
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
