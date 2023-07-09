using System;

namespace VamoPlay.Application.Shared.ViewModels.Response
{
    [Serializable]
    public class LoginResponseViewModel : IViewModel
    {
        public string AccessToken { get; set; }
        public DateTime CreatedIn { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}