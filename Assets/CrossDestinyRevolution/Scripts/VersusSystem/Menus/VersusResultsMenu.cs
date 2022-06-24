using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using CDR.UISystem;

namespace CDR.VersusSystem
{
    public class VersusResultsMenu : Menu, IVersusResultsMenu
    {
        [SerializeField]
        TMP_Text _WinnerText;

        public event Action rematchEvent;
        public event Action returnToMainMenuEvent;

        public IVersusResults results { get; set; }

        public void InvokRematch()
        {
            rematchEvent?.Invoke();
        }

        public void InvokeReturnToMainMenu()
        {
            returnToMainMenuEvent?.Invoke();
        }

        public override void Show()
        {
            base.Show();

            _WinnerText.text = results.winner.name;
        }
    }
}