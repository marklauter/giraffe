﻿using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Documents
{
    internal static class DocumentKeys<TMember>
        where TMember : class
    {
        public static PropertyInfo[] KeyProperties { get; } =
            typeof(TMember).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetCustomAttribute<KeyAttribute>() != null)
                .ToArray();

        public static string TypeName { get; } = typeof(TMember).Name;
    }
}