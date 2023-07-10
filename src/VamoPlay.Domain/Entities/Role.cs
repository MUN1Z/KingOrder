using VamoPlay.Domain.Enums;

namespace VamoPlay.Domain.Entities
{
    public class Role : BaseEntity
    {
        public Role() : base()
        {
            Users = new List<User>();
            UserPermissions = new List<ClaimType>();
        }

        public string Name { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<ClaimType> UserPermissions { get; set; }
    }
}
