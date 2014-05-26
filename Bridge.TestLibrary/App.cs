using Bridge.CLR;
using Bridge.CLR.Html;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{    
    public partial class Class1
    {
        public static void Start()
        {
            var dict = new Dictionary<string, string>();
            dict["test"] = dict["new"];

            var date1 = new DateTime();
            var date2 = new DateTime(2000, 11, 05);

            Console.Log(DateTime.Today.DayOfYear);
            Console.Log("date2", date2.DateData);
        }
    }
}