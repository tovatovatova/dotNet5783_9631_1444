using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    static class StaticClass
    {
        public static string ToStringProperty<T>(this T t)
        {
            string str = "";
            foreach (PropertyInfo item in t.GetType().GetProperties())
            {
                if (item.PropertyType.IsCollectible)
                    str += "\n" + string.Join("", t);
               else
                    str += "\n" + item.Name +
               ": " + item.GetValue(t, null);
            }
               
            return str;
        }
    }
}
