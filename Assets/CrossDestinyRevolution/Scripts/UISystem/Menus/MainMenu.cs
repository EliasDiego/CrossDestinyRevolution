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

        public override void Show()
        {
            _EventSystem.SetSelectedGameObject(_FirstSelect);

            base.Show();
        }

        public void GoToVersus()
        {
            _VersusTransition.Next(this);
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