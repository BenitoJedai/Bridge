using Bridge.CLR;
using Bridge.CLR.Html;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{
    public class App
    {
        public event Action<string> ActionEvent;

        public void Fire()
        {
            this.ActionEvent += App_ActionEvent;
            if (this.ActionEvent != null)
            {
                this.ActionEvent("test");
            }            
        }

        public void App_ActionEvent(string arg)
        {
            throw new NotImplementedException();
        }

        public static void Start()
        {
            
        }
    }   
}