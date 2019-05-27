using System;

namespace JenkinsNET.Internal
{
    internal static class StringExtensions
    {
        public static T To<T>(this string value)
        {
            var tType = typeof(T);

            var nullType = Nullable.GetUnderlyingType(tType);
            var isNullable = nullType != null;

            if (string.IsNullOrEmpty(value))
                return isNullable ? (T)(object)null : default;

            try {
                return (T)value.Cast(isNullable ? nullType : tType);
            }
            catch (Exception error) {
                throw new ApplicationException($"Failed to convert value '{value}' to type '{tType.Name}'!", error);
            }
        }

        private static object Cast(this string value, Type type)
        {
            if (type.IsEnum)
                return Enum.Parse(type, value);

            if (type == typeof(Guid))
                return Guid.Parse(value);

            return Convert.ChangeType(value, type);
        }
    }
}
