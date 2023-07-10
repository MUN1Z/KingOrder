using VamoPlay.Application.Enums;

namespace VamoPlay.Application.ViewModels
{
    [Serializable]
    public class ClaimResponseViewModel : IViewModel
    {
        public string Module { get; set; }

        public IEnumerable<UserPermissionResponseViewModel> Permissions { get; set; }
    }

    [Serializable]
    public class UserPermissionResponseViewModel : IViewModel
    {
        public ClaimType Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
