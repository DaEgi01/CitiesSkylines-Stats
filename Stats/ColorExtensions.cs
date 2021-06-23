using System;
using UnityEngine;

namespace Stats
{
    public static class ColorExtensions
    {
        private const char _serializationSeparator = ',';
        private static readonly char[] _serializationSeparators = new[] { _serializationSeparator };

        public static Color32 GetColor32(this string colorString)
        {
            if (colorString is null)
                throw new ArgumentNullException(nameof(colorString));

            var colorStringComponents = colorString.Split(_serializationSeparators, StringSplitOptions.None);

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
                .Append(_serializationSeparator)
                .Append(color.g)
                .Append(_serializationSeparator)
                .Append(color.b)
                .Append(_serializationSeparator)
                .Append(color.a)
                .ToString();
        }
    }
}
