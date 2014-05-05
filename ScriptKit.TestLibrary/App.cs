using ScriptKit.CLR;
using ScriptKit.CLR.Html;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{
    public class Person
    {
        public string FirstName
        {
            get;
            set;
        }

        public string FullName
        {
            get
            {
                return this.FirstName;
            }
        }
    }

    public class App
    {
        public static string Name = "Geoff";
        public string Field = "Geoff";

        public static void Start()
        {
            if (Window.TypeOf(Window.Arguments) != "undefined")
            {

            }
            Window.AddEventListener(EventType.Click, delegate() { Console.Log("Click", Window.Arguments); });
        }
    }   
}
