using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;

namespace VamoPlay.Application.Shared.Resources
{
    public class CisopResourceManager
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

        #endregion

        #region private members

        private static CisopResourceManager _instance;

        #endregion

        #region public static methods implementations

        public static CisopResourceManager GetInstance()
        {
            if (_instance == null)
                _instance = new CisopResourceManager();

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
