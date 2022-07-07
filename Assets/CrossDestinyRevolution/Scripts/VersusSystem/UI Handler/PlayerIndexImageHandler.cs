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
        private Image _LightImage;

        private bool _IsShown = false;

        public bool isShown => _IsShown;

        private void Awake() 
        {
            _LightImage.enabled = false;
        }

        public void Show()
        {
            _IsShown = true;

            _LightImage.enabled = true;
        }

        public void Hide()
        {
            _IsShown = false;

            _LightImage.enabled = false;
        }
    }
}