using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace CDR.VersusSystem
{
    public class RoundTimeUIHandler : MonoBehaviour, IRoundTimeUIHandler
    {
        [SerializeField]
        Image _Image; 
        [SerializeField]
        TMP_Text _Text;

        private bool _IsShown = false;

        public string roundTimeText { get => _Text.text; set => _Text.text = value; }

        public bool isShown => _IsShown;

        public void Show()
        {
            _Image.enabled = true;
            _Text.enabled = true;
            _IsShown = true;
        }

        public void Hide()
        {
            _Image.enabled = false;
            _Text.enabled = false;
            _IsShown = false;
        }
    }
}