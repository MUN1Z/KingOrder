namespace VamoPlay.Domain.Entities
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            Guid = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public Guid Guid { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
