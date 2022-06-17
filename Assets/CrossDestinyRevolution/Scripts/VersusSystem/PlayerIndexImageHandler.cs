using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using CDR.UISystem;

namespace CDR.VersusSystem
{
    [RequireComponent(typeof(Image))]
    public class PlayerIndexImageHandler : MonoBehaviour, IUIElement
    {
        [SerializeField]
        private Gradient _Gradient;

        private Image _Image;

        private bool _IsShown = false;

        public bool isShown => _IsShown;

        private void Awake() 
        {
            _Image = GetComponent<Image>();    
        }

        public void Show()
        {
            _IsShown = true;

            _Image.color = _Gradient.Evaluate(1);
        }

        public void Hide()
        {
            _IsShown = false;

            _Image.color = _Gradient.Evaluate(0);
        }
    }
}