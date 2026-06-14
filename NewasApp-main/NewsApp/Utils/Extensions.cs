using System.Text.Json;

namespace NewsApp.Utils
{
    public static class Extensions
    {
        public static string ToJson(this object value)
        {
            return JsonSerializer.Serialize(value);
        }

        public static T ToObject<T>(this string value, object defaultValue = null)
        {
            if (string.IsNullOrWhiteSpace(value))
                return (T)defaultValue;

            return JsonSerializer.Deserialize<T>(value, JsonSerializerOptions.Web);
        }
    }
}
