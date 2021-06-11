using System;
using System.Collections.Generic;
using System.Text;

namespace Mdm.Core.Extensions
{
    internal static class DateTmeExtensions
    {
        public static string ToTag(this DateTime date)
        {
            return $"{date.Year.ToString()}{date.Month.ToString().PadLeft(2, '0')}{date.Day.ToString().PadLeft(2,'0')}{date.Hour.ToString().PadLeft(2,'0')}{date.Minute.ToString().PadLeft(2,'0')}";
        }
    }
}
