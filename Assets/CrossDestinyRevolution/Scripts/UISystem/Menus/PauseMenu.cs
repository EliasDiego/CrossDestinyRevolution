using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.UISystem
{
    public class PauseMenu : AnimatedMenu
    {
        private bool _IsPaused = false;

        public bool isPaused => _IsPaused;

        public void Activate()
        {
            Time.timeScale = 0;

            _IsPaused = true;

            Show();
        }

        public void Deactivate()
        {
            Time.timeScale = 1;

            _IsPaused = false;

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