using ScriptKit.CLR;
using ScriptKit.CLR.Html;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{


    public static class App
    {
        public static void Start()
        {
            Window.AddEventListener(EventType.DOMContentLoaded, delegate(Event e) { Console.Log(ScriptKit.CLR.Core.Is(e, "MouseEvent")); });
        }
    }   
}
