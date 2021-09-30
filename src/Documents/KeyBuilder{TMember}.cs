using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Documents
{
    public static class KeyBuilder<TMember>
        where TMember : class
    {

        public static string GetKey(TMember member)
        {
            return KeyProperties.Length > 0
                ? BuildKey(member)
                : throw new ArgumentException($"Argument {nameof(member)} of type {typeof(TMember).FullName} has no properties targeted with [{typeof(KeyAttribute).FullName}].");
        }

        internal static PropertyInfo[] KeyProperties { get; } = typeof(TMember).GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.GetCustomAttribute<KeyAttribute>() != null)
            .ToArray();

        internal static string BuildKey(TMember member)
        {
            var values = new object[KeyProperties.Length];
            for (var i = 0; i < values.Length; ++i)
            {
                values[i] = KeyProperties[i].GetValue(member);
            }

            return KeyBuilder.GetKey(values);
        }
    }
}
