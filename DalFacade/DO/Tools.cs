using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    static class Tools
    {
        public static string ToStringProperty<T>(T t)
        {
            string str = "";
            foreach (PropertyInfo item in t.GetType().GetProperties())
            {
                if (item is IEnumerable<object>)
                {
                    str +="\n"+item.Name+": "+String.Join(" ",item);
                }
                str += "\n" + item.Name
               + ": " + item.GetValue(t, null);
            }
            return str;
        }
    }
}
