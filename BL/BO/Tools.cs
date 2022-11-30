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
                str += "\n" + item.Name+": ";
                if (item.GetValue(t, null) is IEnumerable<object>)//case of IEnumerable property
                
                {
                    IEnumerable<object> lst = (IEnumerable<object>)item.GetValue(obj:t, null);
                    string s = String.Join("  ", lst);
                    str +=  s;
                }
                else
                    str +=item.GetValue(t, null);
            }
            return str+"\n";

        }
        //foreach (PropertyInfo item in t.GetType().GetProperties())
        //{
        //    if (!(item is IEnumerable<object>))
        //    {
        //        str += "\n" + item.Name
        //     + ": " + item.GetValue(t, null) + "\n";
        //    }
        //    else
        //    {
        //        str += "\n" + item.Name + ":";
        //        foreach (var item1 in (IEnumerable<object>)item)
        //        {
        //            str += item.ToString();
        //        }
        //    }

    }

}
