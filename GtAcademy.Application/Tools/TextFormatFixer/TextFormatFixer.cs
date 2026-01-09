using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GtAcademy.Application.Tools.TextFormatFixer
{
    public static class TextFormatFixer
    {
        public static string FixDate(this DateTime date)
        {
            return $"{date.Year}/{date.Month}/{date.Day}";
        }
    }
}
