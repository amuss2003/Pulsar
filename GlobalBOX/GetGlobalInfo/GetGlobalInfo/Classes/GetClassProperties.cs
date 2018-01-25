using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;  // reflection namespace

namespace Pulsar.Classes
{
    public static class GetClassProperties
    {
        //public static void ReadProperties(object obj_class)
        //{
        //    // get all public static properties of MyClass type
        //    PropertyInfo[] propertyInfos;
        //    propertyInfos = typeof(TochenTnua).GetProperties(BindingFlags.Public | BindingFlags.Static);
        //    //propertyInfos = typeof(obj_class).GetProperties(BindingFlags.Public | BindingFlags.Static);
        //    //propertyInfos = obj_class.GetType().GetProperties(BindingFlags.Public | BindingFlags.Static);
        //    // sort properties by name
        //    Array.Sort(propertyInfos, delegate(PropertyInfo propertyInfo1, PropertyInfo propertyInfo2)
        //            { return propertyInfo1.Name.CompareTo(propertyInfo2.Name); });

        //    // write property names
        //    foreach (PropertyInfo propertyInfo in propertyInfos)
        //    {
        //        Console.WriteLine(propertyInfo.Name);
        //    }
        //}

        public static void ReadProperties(object obj_class)
        {
            //TochenTnua
            //foreach (PropertyInfo p in typeof(obj_class).GetProperties())
            foreach (PropertyInfo p in obj_class.GetType().GetProperties())
            {
                Console.WriteLine(p.Name);
                if (p.Name == "MyProperty")
                {

                    //return p 
                }
            }
        }
    }
}
