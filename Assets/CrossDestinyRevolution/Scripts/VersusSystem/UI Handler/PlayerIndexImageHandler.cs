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

        public void Show()
        {
            _IsShown = true;

            _LightImage.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _IsShown = false;

            _LightImage.gameObject.SetActive(false);
        }
    }
}