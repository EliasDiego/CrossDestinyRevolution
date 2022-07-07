using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.UISystem
{
    public class MainMenu : Menu
    {
        [SerializeField]
        private GameObject _Environment;

        public override void Show()
        {
            base.Show();

            _Environment.SetActive(true);
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