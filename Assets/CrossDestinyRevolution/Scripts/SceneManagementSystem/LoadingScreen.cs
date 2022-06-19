using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using CDR.UISystem;
using CDR.VFXSystem;

namespace CDR.SceneManagementSystem
{
    public class LoadingScreen : MonoBehaviour, IUIElement
    {
        [SerializeField]
        private Image _Panel;
        [SerializeField]
        private Gradient _FadeGradient;
        [SerializeField]
        private float _FadeInTime;
        [SerializeField]
        private float _FadeOutTime;

        private Coroutine _ShowCoroutine;
        private Coroutine _HideCoroutine;

        private bool _IsShown = false;

        public bool isShown => _IsShown;

        private void OnFadeEvent(float fadeValue)
        {
            _Panel.color = _FadeGradient.Evaluate(fadeValue);
        }

        private void OnAfterFadeIn()
        {
            _IsShown = true;
        }

        private void OnAfterFadeOut()
        {
            _IsShown = false;
        }

        public void Hide()
        {
            if(_HideCoroutine != null)
                StopCoroutine(_HideCoroutine);

            _HideCoroutine = StartCoroutine(VFXUtilities.LinearEaseOut(OnFadeEvent, _FadeOutTime, OnAfterFadeOut));
        }

        public void Show()
        {
            if(_ShowCoroutine != null)
                StopCoroutine(_ShowCoroutine);

            _ShowCoroutine = StartCoroutine(VFXUtilities.LinearEaseIn(OnFadeEvent, _FadeInTime, OnAfterFadeIn));
        }
    }
}