using System;
using UnityEngine;

namespace Stats
{
    public static class ColorExtensions
    {
        private const char serializationSeparator = ',';
        private static readonly char[] serializationSeparators = new[] { serializationSeparator };

        public static Color32 GetColor32(this string colorString)
        {
            if (colorString == null)
            {
                throw new ArgumentNullException(nameof(colorString));
            }

            var colorStringComponents = colorString.Split(serializationSeparators, StringSplitOptions.None);

            var red = byte.Parse(colorStringComponents[0]);
            var green = byte.Parse(colorStringComponents[1]);
            var blue = byte.Parse(colorStringComponents[2]);
            var transperency = byte.Parse(colorStringComponents[3]);

            return new Color32(red, green, blue, transperency);
        }

        public static string GetColorString(this Color32 color)
        {
            return StringBuilderSingleton.Instance
                .Append(color.r)
                .Append(serializationSeparator)
                .Append(color.g)
                .Append(serializationSeparator)
                .Append(color.b)
                .Append(serializationSeparator)
                .Append(color.a)
                .ToString();
        }
    }
}
