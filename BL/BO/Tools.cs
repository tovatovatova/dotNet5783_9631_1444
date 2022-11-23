using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   static class Tools
    {
        //help reflaction method to to concatenate strings to describe object 
        public static string ToStringProperty<T>(this T t)
        {
            string str = "";
            foreach (PropertyInfo item in t.GetType().GetProperties())
            {
                if (item is IEnumerable<object>)//specal case o IEnumerable property
                {
                    str += "\n" + item.Name + ": " + String.Join(" ", item);
                }
                str += "\n" + item.Name
               + ": " + item.GetValue(t, null);
            }
            return str;
        }
    }
}
