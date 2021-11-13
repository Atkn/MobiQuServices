using System;
using System.Collections.Generic;
using System.Text;

namespace MobiQu.Services.Application.Common.Utilities
{
    public class EntityUtilities<TEntity> where TEntity : class, new()
    {
        public static string DateTimeFormater(DateTime? dateTime)
        {
            string dateTimeString = "";
            if (dateTime != null && dateTime != DateTime.MinValue)
                dateTimeString = dateTime.Value.ToString("dd/MM/yyyy HH:MM");
            else
                dateTimeString = "-";
            return dateTimeString;
            
        }
    }
}
