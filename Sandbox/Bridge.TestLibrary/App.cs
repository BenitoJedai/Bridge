using Bridge.CLR;
using Bridge.Html5;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{    
    public partial class Class1
    {
        public static void Start()
        {
            Window.OnLoad = delegate {
                var txt = new InputElement()
                {
                    Value = "Hello Input",
                    Style =
                    {
                        Width = "239px",
                        Color = HTMLColor.Red
                    }
                };

                Document.Body.AppendChild(txt);
            };            
        }
    }
}