using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.InputSystem
{
    public class AIAction
    {
        public event Action action; 

        public string name { get; }
        public bool enabled { get; set; }

        public AIAction(string name, Action action)
        {
            this.name = name;
            this.action = action;

            enabled = false;
        }
        
        public void Invoke()
        {
            if(enabled)
                action?.Invoke();
        }
    }
}