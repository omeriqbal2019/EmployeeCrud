using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Utilities.FormatData
{
    public static class DataSplit
    {

        public static string FilterData(string str)
        {
            return str.Replace("[", "").Replace("]", "");
            // return str.Replace("[", "").Replace("]", "").Split(",");
        }
    }
}
