using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AlephonAssignment.Helpers
{
    public static class StringHelper
    {
        public static string ToString(this List<double> list, string _numStringFormat)
        {
            var str = new StringBuilder();
            foreach (var n in list)
            {
                str.Append(string.Format(_numStringFormat, n.ToString()));
            }

            return str.ToString(); 
        }

        public static string ToString(this double[] list, string _numStringFormat)
        {
            var str = new StringBuilder();
            //Parallel.For(0, list.Length, n =>
            //{
            //    str.Append(string.Format(_numStringFormat, n.ToString()));
            //});
            foreach (var n in list)
            {
                str.Append(string.Format(_numStringFormat, n.ToString()));
            }

            return str.ToString();
        }

        public static StringBuilder RemoveLast(this StringBuilder sb, string value)
        {
            if (sb.Length < 1) return sb;
            sb.Remove(sb.ToString().LastIndexOf(value), value.Length);
            return sb;
        }
    }
}
