namespace VamoPlay.Domain.Entities
{
    public class TournamentCategory : BaseEntity
    {
        public Guid TournamentGuid;
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Tournament Tournament { get; set; }
    }
}
