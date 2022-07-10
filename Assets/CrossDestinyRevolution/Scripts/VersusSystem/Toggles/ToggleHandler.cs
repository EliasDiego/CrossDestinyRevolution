using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace CDR.VersusSystem
{
    public class ToggleHandler : MonoBehaviour
    {
        private Toggle _Toggle;

        public Toggle toggle => _Toggle;

        protected virtual void Awake() 
        {
            _Toggle = GetComponent<Toggle>();
        }
    }
}