using System;

namespace VamoPlay.Application.Shared.ViewModels.Response
{
    [Serializable]
    public class UserAccountResponseViewModel : IViewModel
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? LastAccess { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
