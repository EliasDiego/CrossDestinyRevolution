using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

using CDR.AudioSystem;

namespace CDR.UISystem
{
    public class TitleScreenMenu : AnimatedMenu
    {
        [SerializeField]
        InputActionReference _StartActionReference;
        [SerializeField]
        EventSystem _EventSystem;
        [SerializeField]
        Transition _MainMenuTransition;
        [SerializeField]
        AudioClipPreset _StartSFX;
        [SerializeField]
        AudioSource _AudioSource;

        protected override void Awake()
        {
            base.Awake();

            _EventSystem.gameObject.SetActive(false);
        }

        private void OnStart(InputAction.CallbackContext context)
        {
            _MainMenuTransition.Next(this);
        }

        protected override IEnumerator HideAnimatedSequence()
        {
            _StartSFX?.PlayOneShot(_AudioSource);

            _StartActionReference.action.Disable();

            _StartActionReference.action.started -= OnStart;

            yield return base.HideAnimatedSequence();

            _EventSystem.gameObject.SetActive(true);
        }

        public override void Show()
        {
            base.Show();

            _StartActionReference.action.Enable();

            _StartActionReference.action.started += OnStart;
        }
    }
}