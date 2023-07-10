using VamoPlay.Application.Enums;
using VamoPlay.Application.Resources;

namespace VamoPlay.Application.Attributes
{
    public class LocalizedUserClaimValuesAttribute : Attribute
    {
        #region properties

        public ClaimType Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Module { get; set; }

        #endregion

        #region constructors

        public LocalizedUserClaimValuesAttribute(string name, string description, string module)
        {
            Name = VamoPlayResourceManager.GetInstance().GetMessageFromResource(name);
            Description = VamoPlayResourceManager.GetInstance().GetMessageFromResource(description);
            Module = VamoPlayResourceManager.GetInstance().GetMessageFromResource(module);
        }

        #endregion
    }
}
