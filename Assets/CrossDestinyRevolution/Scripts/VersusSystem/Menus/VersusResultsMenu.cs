using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using TMPro;

using CDR.UISystem;

namespace CDR.VersusSystem
{
    public class VersusResultsMenu : AnimatedMenu, IVersusResultsMenu
    {
        [SerializeField]
        TMP_Text _WinnerText;
        [SerializeField]
        EventSystem _EventSystem;
        [SerializeField]
        GameObject _FirstSelect;

        public event Action rematchEvent;
        public event Action returnToMainMenuEvent;

        public IVersusResults results { get; set; }

        public void InvokRematch()
        {
            Hide();
        }

        protected override IEnumerator HideAnimatedSequence()
        {
            yield return base.HideAnimatedSequence();

            rematchEvent?.Invoke();
        }

        public void InvokeReturnToMainMenu()
        {
            returnToMainMenuEvent?.Invoke();
        }

        public override void Show()
        {
            base.Show();

            _EventSystem.SetSelectedGameObject(_FirstSelect);

            _WinnerText.text = results.winner.name;
        }
    }
}