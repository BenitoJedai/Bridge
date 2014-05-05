using ScriptKit.CLR;
using ScriptKit.CLR.Html;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{
    public class App
    {
        public const string VERSION = "1.0.0-beta";
        public string VERSION1 = "1.0.0-beta";

        public static void Start()
        {
            Console.Log(VERSION);
            Console.Log(App.VERSION);

            var app = new App();
            Console.Log(app.VERSION1);
        }
    }   
}
