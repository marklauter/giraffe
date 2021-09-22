using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace Documents
{
    public static class KeyBuilder<TMember>
        where TMember : class
    {
        [Pure]
        public static string GetKey([Pure] TMember member)
        {
            return KeyProperties.Length > 0
                ? BuildKey(member)
                : throw new ArgumentException($"Argument {nameof(member)} of type {typeof(TMember).FullName} has no properties targeted with [{typeof(KeyAttribute).FullName}].");
        }

        internal static PropertyInfo[] KeyProperties { get; } =
            typeof(TMember).GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.GetCustomAttribute<KeyAttribute>() != null)
            .ToArray();

        internal static string BuildKey([Pure] TMember member)
        {
            var keys = new string[KeyProperties.Length];
            for (var i = 0; i < keys.Length; ++i)
            {
                keys[i] = KeyProperties[i].GetValue(member).ToString();
            }

            return String.Join('.', keys);
        }
    }
}
