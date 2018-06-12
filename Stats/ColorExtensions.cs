using System;
using System.Text;
using UnityEngine;

namespace Stats
{
    public static class ColorExtensions
    {
        public static Color32 GetColor32(this string colorString)
        {
            if (colorString == null)
            {
                throw new ArgumentNullException(nameof(colorString));
            }

            var colorStringComponents = colorString.Split(',');

            var red = byte.Parse(colorStringComponents[0]);
            var green = byte.Parse(colorStringComponents[1]);
            var blue = byte.Parse(colorStringComponents[2]);
            var transperency = byte.Parse(colorStringComponents[3]);

            return new Color32(red, green, blue, transperency);
        }

        public static string GetColorString(this Color32 color)
        {
            var sb = new StringBuilder();

            sb.Append(color.r.ToString());
            sb.Append(",");
            sb.Append(color.g.ToString());
            sb.Append(",");
            sb.Append(color.b.ToString());
            sb.Append(",");
            sb.Append(color.a.ToString());

            return sb.ToString();
        }
    }
}
