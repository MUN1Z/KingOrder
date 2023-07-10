using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;

namespace VamoPlay.Application.Resources
{
    public class VamoPlayResourceManager
    {
        #region constants

        public const string NameIsRequired = "name_is_required";
        public const string NameMaxLength = "name_max_length";

        public const string EmailIsRequired = "email_is_required";
        public const string InvalidEmailFormat = "invalid_email_format";
        public const string EmailMaxLength = "email_max_length";

        public const string PasswordIsRequired = "password_is_required";
        public const string NewPasswordIsRequired = "new_password_is_required";
        public const string ConfirmNewPasswordIsRequired = "confirm_new_password_is_required";
        public const string PasswordMaxLength = "password_max_length";

        public const string DescriptionMaxLength = "description_max_length";
        public const string DescriptionIsRequired = "description_is_required";

        public const string PermissionIsRequired = "permission_is_required";

        public const string List = "list";
        public const string Edit = "edit";
        public const string Register = "register";
        public const string Remove = "remove";
        public const string ActivateDeactivate = "activate_deactivate";
        public const string ListRecords = "list_records";
        public const string EditRecords = "edit_records";
        public const string RegisterRecords = "register_records";
        public const string RemoveRecords = "remove_records";
        public const string ActivateDeactivateRecords = "activate_deactivate_records";

        public const string User = "user";
        public const string UserRole = "user_role";
        public const string EmailConfiguration = "email_configuration";
        public const string Tournament = "tournament";
        public const string TournamentCategory = "tournament_category";

        #endregion

        #region private members

        private static VamoPlayResourceManager _instance;

        #endregion

        #region public static methods implementations

        public static VamoPlayResourceManager GetInstance()
        {
            if (_instance == null)
                _instance = new VamoPlayResourceManager();

            return _instance;
        }

        #endregion

        #region public methods implementations

        public string GetMessageFromResource(string resourceKey, params string[] parameters)
        {
            var thread = Thread.CurrentThread.CurrentUICulture.Name;
            var culture = new CultureInfo(thread);
            var resourceManager = new ResourceManager(typeof(Resource));
            string value = resourceManager.GetString(resourceKey, culture);

            if (parameters.Any() && value != null)
            {
                value = string.Format(value, parameters);
            }

            return value != null ? value : "";
        }

        #endregion
    }
}
