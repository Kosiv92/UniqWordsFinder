using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSupport.Extensions
{
    public static class StringExtensions
    {
        public static string DeleteSupplySymbols(this string str, string[] searchedSymbols)
        {

            foreach (string c in searchedSymbols)
            {
                str = str.Replace(c, "").Trim(' ');
            }
            return str;
        }
    }
}
