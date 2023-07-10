using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VamoPlay.Application.ViewModels.Response
{
    [Serializable]
    public class SelectDataResponseViewModel : IViewModel
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public dynamic? Data { get; set; }
    }
}
