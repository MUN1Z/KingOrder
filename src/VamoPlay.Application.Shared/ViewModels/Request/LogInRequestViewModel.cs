using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VamoPlay.Application.Shared.ViewModels.Response
{
    [Serializable]
    public class LogInRequestViewModel : IViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }

        public IList<object> ExternalLogins { get; set; }
    }
}
