using Bridge.CLR;
using Bridge.CLR.Html;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{
    public class App
    {
        public static void Method(string s = "test", int i = 5)
        {

        }
        
        public static void Start()
        {
            Method();
            Method("t");
        }
    }   
}

namespace Js
{
    [Name("DateTime")]
    public class DateTime
    {
        public const string VERSION = "2.0.0-beta";

        public static string GetVersion()
        {
            return DateTime.VERSION;
        }

        public Date Date;

        public Date ToDate()
        {
            return this.Date;
        }
    }
}
