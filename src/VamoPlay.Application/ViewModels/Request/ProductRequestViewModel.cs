using System;
using System.ComponentModel.DataAnnotations;

namespace VamoPlay.Application.ViewModels.Response
{
    [Serializable]
    public class ProductRequestViewModel : IViewModel
    {
        [Required(ErrorMessage = "Gtin is required!")]
        public string Gtin { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required!")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Discount is required!")]
        public decimal Discount { get; set; }

        [Required(ErrorMessage = "Thumb is required!")]
        public string Thumb { get; set; }
    }
}
