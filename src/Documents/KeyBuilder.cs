using System;

namespace Documents
{
    public static class KeyBuilder
    {
        public static string GetKey(params object[] values)
        {
            var keys = new string[values.Length];
            for (var i = 0; i < keys.Length; ++i)
            {
                keys[i] = values[i].ToString();
            }

            return String.Join('.', keys);
        }
    }
}
