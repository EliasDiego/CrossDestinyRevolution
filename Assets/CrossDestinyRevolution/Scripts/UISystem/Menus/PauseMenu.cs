using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.UISystem
{
    public class PauseMenu : Menu
    {
        public void Activate()
        {
            Time.timeScale = 0;

            Show();
        }

        public void Deactivate()
        {
            Time.timeScale = 1;

            Hide();
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