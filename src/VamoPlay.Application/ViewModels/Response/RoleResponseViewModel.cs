﻿using VamoPlay.Application.Enums;

namespace VamoPlay.Application.ViewModels
{
    [Serializable]
    public class RoleResponseViewModel : IViewModel
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<ClaimType> UserPermissions { get; set; }
    }
}