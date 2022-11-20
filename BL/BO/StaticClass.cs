using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   static class StaticClass
    {
        public static string ToStringProperty<T>(this T t)//785768978979(*&*(&(*&(((((((((((((
        {
            string str = "";
            foreach (PropertyInfo item in t.GetType().GetProperties())
                str += "\n" + item.Name +
                ": " + item.GetValue(t, null);
            return str;
        }
    }
}
