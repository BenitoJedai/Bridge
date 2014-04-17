using ScriptKit.CLR;
using ScriptKit.CLR.Html;
using ScriptKit.TestLibrary;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{
    public static class App
    {
        /*public static void Start()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("item1", 1);
            dict.Add("item2", 2);
            dict.Add("item3", 3);

            object obj = dict["Item2"];
            object obj1 = new Dictionary<string, object>().Count;

            foreach (var item in dict)
            {
                Console.Log(item.Key + " - " + item.Value);
            }
        }*/

        public static void Start()
        {
            List<string> list = new List<string>(new string[]{
                "Item1",
                "Item2",
                "Item3"
            });

            list.Remove("Item2");

            foreach (var item in list)
            {
                Console.Log(item);
            }
        }

    }   
}
