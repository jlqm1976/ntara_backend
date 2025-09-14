using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeFootball.Domain.Utils
{
    public static class MiscUtils
    {
        public static DateTime? GetDateValue(string value)
        {
            if (DateTime.TryParse(value, out DateTime result))
            {
                return result;
            }

            return null;
        }

        public static float? GetFloatValue(string value)
        {
            if (float.TryParse(value, out float result))
            {
                return result;
            }

            return null;
        }

        public static int? GetIntValue(string value)
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }

            return null;
        }
    }
}
