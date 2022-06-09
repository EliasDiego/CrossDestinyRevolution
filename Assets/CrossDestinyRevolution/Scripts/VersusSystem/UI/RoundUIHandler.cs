using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace CDR.VersusSystem
{
    public class RoundUIHandler : MonoBehaviour, IRoundUIHandler
    {
        [SerializeField]
        private Image _Image;

        private bool _IsShown = false;

        public bool isShown => _IsShown;

        public void Hide()
        {
            _IsShown = false;
            
            _Image.gameObject.SetActive(false);
        }

        public void Show()
        {
            _IsShown = true;

            _Image.gameObject.SetActive(true);
        }
    }
}