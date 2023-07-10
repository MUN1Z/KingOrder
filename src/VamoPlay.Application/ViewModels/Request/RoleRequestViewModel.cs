using VamoPlay.Application.Attributes;
using VamoPlay.Application.Constants;
using VamoPlay.Application.Enums;
using VamoPlay.Application.Resources;
using VamoPlay.Application.ViewModels;

namespace VamoPlay.Application.ViewModels
{
    [Serializable]
    public class RoleRequestViewModel : IViewModel
    {
        [LocalizedRequired(VamoPlayResourceManager.NameIsRequired)]
        [LocalizedMaxLength(RequestConstants.Length255, VamoPlayResourceManager.NameMaxLength)]
        public string Name { get; set; }

        [LocalizedMaxLength(RequestConstants.Length1000, VamoPlayResourceManager.DescriptionMaxLength)]
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [LocalizedRequired(VamoPlayResourceManager.PermissionIsRequired)]
        public ICollection<ClaimType> UserPermissions { get; set; }
    }
}
