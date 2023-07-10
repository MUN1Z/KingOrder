namespace VamoPlay.Domain.Entities
{
    public class Tournament : BaseEntity
    {
        public Guid UserGuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartInscriptionDate { get; set; }
        public DateTime EndInscriptionDate { get; set; }
        public string Thumb { get; set; }
        public string Banner { get; set; }
        public virtual User User { get; set; }
    }
}
