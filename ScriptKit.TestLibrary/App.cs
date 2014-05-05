using ScriptKit.CLR;
using ScriptKit.CLR.Html;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{
    public class App
    {
        public static void Start()
        {
            var date = new DateTime();
        }
    }   
}

namespace Js
{
    [Name("DateTime")]
    public static class DateTime
    {
        public const string VERSION = "1.0.0-beta";

        public static string GetVersion()
        {
            return VERSION;
        }

        public static string GetName()
        {
            return "DateTimeJS";
        }
    }
}
