using System;

namespace VamoPlay.Application.ViewModels.Response
{
    [Serializable]
    public class TournamentResponseViewModel : IViewModel
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartInscriptionDate { get; set; }
        public DateTime EndInscriptionDate { get; set; }
        public string Thumb { get; set; }
        public string Banner { get; set; }
    }
}
