using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.UISystem
{
    public class PauseMenu : AnimatedMenu
    {
        private bool _IsPaused = false;

        public bool isPaused => _IsPaused;
        public event Action<bool> onActivate;

        public void Activate()
        {
            Time.timeScale = 0;

            _IsPaused = true;

            onActivate?.Invoke(true);

            Show();
        }

        public void Deactivate()
        {
            Time.timeScale = 1;

            _IsPaused = false;

            onActivate?.Invoke(false);

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