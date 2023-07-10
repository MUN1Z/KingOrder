using VamoPlay.Application.Enums;
using VamoPlay.Application.Attributes;

namespace VamoPlay.Application.Extensions
{
    public static class EnumExtensions
    {
        #region extensions

        public static TAttribute GetAttribute<TAttribute>(this Enum value)
        where TAttribute : Attribute
        {
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value);
            return enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }

        public static LocalizedUserClaimValuesAttribute GetUserClaimValuesAttribute(this ClaimType value)
        {
            var attribute = value.GetAttribute<LocalizedUserClaimValuesAttribute>();
            attribute.Key = value;
            return attribute;
        }

        #endregion
    }
}
