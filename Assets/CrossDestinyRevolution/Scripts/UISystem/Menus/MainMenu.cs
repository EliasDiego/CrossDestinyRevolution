using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.UISystem
{
    public class MainMenu : Menu
    {
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