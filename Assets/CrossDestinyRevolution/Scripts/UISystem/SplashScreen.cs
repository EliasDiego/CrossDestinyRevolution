using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CDR.UISystem
{
    public class SplashScreen : MonoBehaviour, IUIElement
    {
        public bool isShown => false;

        private void Awake() 
        {
               
        }

        public void Hide()
        {
            // _Director.Stop();
        }

        public void Show()
        {
            // _Director.Play();
        }
    }
}