using Bridge.CLR;
using Bridge.CLR.Html;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{    
    public partial class Class1
    {
        public int field;

        public void Method1()
        {
            var btn = Document.GetElementById("btn1");

            if (btn == null)
            {
                Window.OnLoad = delegate(Event e) {
                    this.Method1();
                };

                return;
            }
            
            btn.OnClick = btn.OnClick + delegate(Event e) {
                field = 0;
                Window.Alert("click1");
            };

            btn.OnClick += e => Window.Alert("click3");

            Action<Event> handler = delegate(Event e)
            {
                field = 0;
                Window.Alert("click2");
            };

            btn.OnClick = btn.OnClick + handler;
            btn.OnClick = btn.OnClick - handler;
        }
        
        
        public static void Start()
        {
            var c = new Class1();
            c.Method1();
        }
    }
}