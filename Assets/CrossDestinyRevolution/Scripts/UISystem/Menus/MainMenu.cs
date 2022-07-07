using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

namespace CDR.UISystem
{
    public class MainMenu : Menu
    {
        [SerializeField]
        private GameObject _Environment;
        [SerializeField]
        private EventSystem _EventSystem;
        [SerializeField]
        GameObject _FirstSelect;

        public override void Show()
        {
            base.Show();

            _Environment.SetActive(true);

            _EventSystem.SetSelectedGameObject(_FirstSelect);
        }

        public override void Hide()
        {
            base.Hide();

            _Environment.SetActive(false);
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