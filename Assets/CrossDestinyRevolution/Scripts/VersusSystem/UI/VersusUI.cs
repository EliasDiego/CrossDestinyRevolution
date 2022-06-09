using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace CDR.VersusSystem
{
    public class VersusUI : MonoBehaviour, IVersusUI
    {
        [SerializeField]
        RoundUIHandler _RoundUIHandler;
        [SerializeField]
        RoundTimeUIHandler _RoundTimeUIHandler;

        private bool _IsShown = false;

        public IRoundUIHandler roundUIHandler => _RoundUIHandler;

        public IRoundTimeUIHandler roundTimeUIHandler => _RoundTimeUIHandler;

        public bool isShown => _IsShown;

        public void Show()
        {
            _IsShown = true;

            roundUIHandler.Show();
            roundTimeUIHandler.Show();
        }

        public void Hide()
        {
            _IsShown = false;

            roundUIHandler.Hide();
            roundTimeUIHandler.Hide();
        }
    }
}