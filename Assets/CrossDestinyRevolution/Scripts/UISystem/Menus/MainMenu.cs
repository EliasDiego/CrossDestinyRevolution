using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

namespace CDR.UISystem
{
    public class MainMenu : AnimatedMenu
    {
        [SerializeField]
        private GameObject _Environment;
        [SerializeField]
        private EventSystem _EventSystem;
        [SerializeField]
        GameObject _FirstSelect;

        protected override IEnumerator HideAnimatedSequence()
        {
            yield return base.HideAnimatedSequence();

            _Environment.SetActive(false);
        }

        public override void Show()
        {
            _Environment.SetActive(true);

            _EventSystem.SetSelectedGameObject(_FirstSelect);

            base.Show();
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