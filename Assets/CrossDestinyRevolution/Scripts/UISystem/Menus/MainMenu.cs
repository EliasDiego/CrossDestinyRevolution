using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

namespace CDR.UISystem
{
    public class MainMenu : AnimatedMenu
    {
        [SerializeField]
        private EventSystem _EventSystem;
        [SerializeField]
        GameObject _FirstSelect;
        [SerializeField]
        Transition _VersusTransition;
        [SerializeField]
        Transition _CreditsTransition;

        public override void Show()
        {
            _EventSystem.SetSelectedGameObject(_FirstSelect);

            base.Show();
        }

        public override void Hide()
        {
            _EventSystem.SetSelectedGameObject(null);

            base.Hide();
        }

        public void GoToVersus()
        {
            _VersusTransition.Next(this);
        }

        public void GoToCredits()
        {
            _CreditsTransition.Next(this);
        }

        public void Quit()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit(0);
            #endif
        }
    }
}