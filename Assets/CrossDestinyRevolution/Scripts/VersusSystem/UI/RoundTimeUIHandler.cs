using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

namespace CDR.VersusSystem
{
    public class RoundTimeUIHandler : MonoBehaviour, IRoundTimeUIHandler
    {
        [SerializeField]
        TMP_Text _Text;

        private bool _IsShown = false;

        public string roundTimeText { get => _Text.text; set => _Text.text = value; }

        public bool isShown => _IsShown;

        public void Show()
        {
            _IsShown = true;
        }

        public void Hide()
        {
            _IsShown = false;
        }
    }
}