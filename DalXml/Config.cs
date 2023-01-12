using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    internal class Config
    {
        static string s_config = "configuration";
        internal static int NextOrderNumber ()
        {
            return  ((int)XMLTools.LoadListFromXMLElement(s_config).Element("NextOrderId"));
        }
        internal static void SaveNextOrderNumber (int num) 
        {
            XElement root = XMLTools.LoadListFromXMLElement(s_config);
            root.Element("NextOrderId").SetValue(num.ToString());
            XMLTools.SaveListToXMLElement(root,s_config);
        }


        internal static int NextOrderItemNumber()
        {
            return ((int)XMLTools.LoadListFromXMLElement(s_config).Element("NextOrderItemId"));
        }
        internal static void SaveNextOrderItemNumber(int num)
        {
            XElement root = XMLTools.LoadListFromXMLElement(s_config);
            root.Element("NextOrderItemId").SetValue(num.ToString());
            XMLTools.SaveListToXMLElement(root, s_config);
        }








    }
}
