using Bridge.CLR;
using Bridge.Html5;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{
    public partial class Class1
    {

        public delegate void Class1Event(int i);
        public event Class1Event OnEvent = Class1_Event;
        public static event Class1Event OnEvent2 = Class1_Event;

        public static void Class1_Event(int i)
        {
            Console.Log("OnStaticEvent " + i);
        }

        public void InstanceStart()
        {
            this.OnEvent += Class1_OnEvent;
            OnPropEvent += this.Class1_OnPropEvent;

            if (this.OnEvent != null)
            {
                this.OnEvent(1);
            }

            this.OnEvent -= Class1_OnEvent;
            OnPropEvent -= this.Class1_OnPropEvent;

            if (this.OnEvent != null)
            {
                this.OnEvent(2);
            }
        }

        private void Class1_OnEvent(int i)
        {
            Console.Log("OnEvent " + i);
        }

        private void Class1_OnPropEvent(int i)
        {
            Console.Log("OnPropEvent " + i);
        }

        public event Class1Event OnPropEvent
        {
            add
            {
                this.OnEvent += value;
            }
            remove
            {
                this.OnEvent -= value;
            }
        }

        public static void Start()
        {
            OnEvent2(2);
            new Class1().InstanceStart();            
        }
    }
}