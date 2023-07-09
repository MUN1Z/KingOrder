using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace VamoPlay.Application.Extensions
{
    public static class FilterExtensions
    {
        #region public methods implementations

        public static string ToQueryString<TFilter>(this TFilter filter, string enpoint) where TFilter : class
        {
            var queryString = $"api/{enpoint}?";
            var Rfc3339DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss.fffffffK";

            var filtersProperties = typeof(TFilter).GetProperties();
            var dateTimeTypes = new[] { typeof(DateTime), typeof(DateTime?) };

            for (int i = 0; i < filtersProperties.Length; i++)
            {
                var fieldName = filtersProperties[i].Name;
                var fieldValue = filtersProperties[i].GetValue(filter);

                if (fieldValue == null || (filtersProperties[i].PropertyType == typeof(string) && string.IsNullOrEmpty((string)fieldValue)))
                    continue;

                if ((filtersProperties[i].PropertyType.ToString().Contains("Enumerable") ||
                    filtersProperties[i].PropertyType.ToString().Contains("Collection") ||
                    filtersProperties[i].PropertyType.ToString().Contains("List")) &&
                    (filtersProperties[i].PropertyType.ToString().Contains("Guid")))
                    foreach (var itemFieldValue in fieldValue as IEnumerable<Guid>)
                    {
                        var flag = i < (((IEnumerable<Guid>)fieldValue).Count() - 1) ? "&" : "";
                        queryString += $"{fieldName}={itemFieldValue}{flag}";
                    }
                else if (dateTimeTypes.Contains(filtersProperties[i].PropertyType))
                {
                    var flag = i < (filtersProperties.Length - 1) ? "&" : "";
                    var dateTime = (DateTime)fieldValue;
                    var rfc3339DateTime = dateTime.ToString(Rfc3339DateTimeFormat, DateTimeFormatInfo.InvariantInfo);
                    queryString += $"{fieldName}={rfc3339DateTime}{flag}";
                }
                else
                {
                    var flag = i < (filtersProperties.Length - 1) ? "&" : "";
                    queryString += $"{fieldName}={fieldValue}{flag}";
                }
            }

            return queryString;
        }

        #endregion
    }
}
