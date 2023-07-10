using System.ComponentModel.DataAnnotations;

namespace VamoPlay.Application.ViewModels.Response
{
    [Serializable]
    public class TournamentRequestViewModel : IViewModel
    {
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "IsVisible is required!")]
        public bool IsVisible { get; set; }

        [Required(ErrorMessage = "Date is required!")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Date is required!")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Date is required!")]
        public DateTime StartInscriptionDate { get; set; }

        [Required(ErrorMessage = "Date is required!")]
        public DateTime EndInscriptionDate { get; set; }

        [Required(ErrorMessage = "Thumb is required!")]
        public string Thumb { get; set; }

        [Required(ErrorMessage = "Banner is required!")]
        public string Banner { get; set; }
    }
}
