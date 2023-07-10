namespace VamoPlay.Domain.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            Roles = new List<Role>();
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? LastAccess { get; set; }

        //public Guid RoleGuid { get; set; }

        //public virtual Role Role { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
