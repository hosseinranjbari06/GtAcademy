using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GtAcademy.Application.Tools.PersianDateConverter
{
    public static class PersianDateConverter
    {
        public static DateTime ToShamsi(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return new DateTime(pc.GetYear(date), pc.GetMonth(date), pc.GetDayOfMonth(date));
        }
    }
}
