namespace VamoPlay.Domain.Entities
{
    public class Address : BaseEntity
    {
        public Guid UserGuid;
        public string Name { get; set; }
        public string CEP { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Neigborhood { get; set; }
        public virtual User User { get; set; }
    }
}
