using System;
using System.Collections.Generic;

namespace VamoPlay.Application.Shared.ViewModels.Response
{
    [Serializable]
    public class BaseResponseViewModel : IViewModel
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public List<string> Errors { get; set; }
    }
}
