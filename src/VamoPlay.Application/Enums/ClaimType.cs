using VamoPlay.Application.Attributes;
using VamoPlay.Application.Resources;

namespace VamoPlay.Application.Enums
{
    public enum ClaimType
    {
        [LocalizedUserClaimValues(name: VamoPlayResourceManager.List, description: VamoPlayResourceManager.ListRecords, module: VamoPlayResourceManager.User)]
        Users_Read,
        [LocalizedUserClaimValues(name: VamoPlayResourceManager.Edit, description: VamoPlayResourceManager.EditRecords, module: VamoPlayResourceManager.User)]
        Users_Write,
        [LocalizedUserClaimValues(name: VamoPlayResourceManager.Register, description: VamoPlayResourceManager.RegisterRecords, module: VamoPlayResourceManager.User)]
        Users_Create,
        [LocalizedUserClaimValues(name: VamoPlayResourceManager.Remove, description: VamoPlayResourceManager.RemoveRecords, module: VamoPlayResourceManager.User)]
        Users_Delete,
        [LocalizedUserClaimValues(name: VamoPlayResourceManager.ActivateDeactivate, description: VamoPlayResourceManager.ActivateDeactivateRecords, module: VamoPlayResourceManager.User)]
        Users_ActivateDeactivate,
        [LocalizedUserClaimValues(name: VamoPlayResourceManager.List, description: VamoPlayResourceManager.ListRecords, module: VamoPlayResourceManager.Role)]
        Roles_Read,
        [LocalizedUserClaimValues(name: VamoPlayResourceManager.Edit, description: VamoPlayResourceManager.EditRecords, module: VamoPlayResourceManager.Role)]
        Roles_Write,
        [LocalizedUserClaimValues(name: VamoPlayResourceManager.Register, description: VamoPlayResourceManager.RegisterRecords, module: VamoPlayResourceManager.Role)]
        Roles_Create,
        [LocalizedUserClaimValues(name: VamoPlayResourceManager.Remove, description: VamoPlayResourceManager.RemoveRecords, module: VamoPlayResourceManager.Role)]
        Roles_Delete,
        [LocalizedUserClaimValues(name: VamoPlayResourceManager.List, description: VamoPlayResourceManager.ListRecords, module: VamoPlayResourceManager.Tournament)]
        Tournament_Read,
        [LocalizedUserClaimValues(name: VamoPlayResourceManager.Edit, description: VamoPlayResourceManager.EditRecords, module: VamoPlayResourceManager.Tournament)]
        Tournament_Write,
        [LocalizedUserClaimValues(name: VamoPlayResourceManager.Register, description: VamoPlayResourceManager.RegisterRecords, module: VamoPlayResourceManager.Tournament)]
        Tournament_Create,
        [LocalizedUserClaimValues(name: VamoPlayResourceManager.Remove, description: VamoPlayResourceManager.RemoveRecords, module: VamoPlayResourceManager.Tournament)]
        Tournament_Delete,
        [LocalizedUserClaimValues(name: VamoPlayResourceManager.ActivateDeactivate, description: VamoPlayResourceManager.ActivateDeactivateRecords, module: VamoPlayResourceManager.Tournament)]
        Tournament_ActivateDeactivate,
        [LocalizedUserClaimValues(name: VamoPlayResourceManager.List, description: VamoPlayResourceManager.ListRecords, module: VamoPlayResourceManager.TournamentCategory)]
        TournamentCategory_Read,
        [LocalizedUserClaimValues(name: VamoPlayResourceManager.Edit, description: VamoPlayResourceManager.EditRecords, module: VamoPlayResourceManager.TournamentCategory)]
        TournamentCategory_Write,
        [LocalizedUserClaimValues(name: VamoPlayResourceManager.Register, description: VamoPlayResourceManager.RegisterRecords, module: VamoPlayResourceManager.TournamentCategory)]
        TournamentCategory_Create,
        [LocalizedUserClaimValues(name: VamoPlayResourceManager.Remove, description: VamoPlayResourceManager.RemoveRecords, module: VamoPlayResourceManager.TournamentCategory)]
        TournamentCategory_Delete,
        [LocalizedUserClaimValues(name: VamoPlayResourceManager.ActivateDeactivate, description: VamoPlayResourceManager.ActivateDeactivateRecords, module: VamoPlayResourceManager.TournamentCategory)]
        TournamentCategory_ActivateDeactivate,
    }
}
