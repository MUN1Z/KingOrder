using VamoPlay.Application.Enums;

namespace VamoPlay.Application.ViewModels
{
    [Serializable]
    public class UserClaimResponseViewModel : IViewModel
    {
        public string Module { get; set; }

        public IEnumerable<UserPermissionResponseViewModel> Permissions { get; set; }
    }

    [Serializable]
    public class UserPermissionResponseViewModel : IViewModel
    {
        public UserClaim Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
