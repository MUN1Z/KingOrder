using VamoPlay.Domain.Enums;

namespace VamoPlay.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public UserRole() : base()
        {
            Users = new List<User>();
            UserPermissions = new List<UserClaim>();
        }

        public string Name { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<UserClaim> UserPermissions { get; set; }
    }
}
