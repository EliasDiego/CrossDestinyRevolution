using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using CDR.VFXSystem;
using CDR.InputSystem;

namespace CDR.UISystem
{
    public class SplashScreen : Menu, IMenuSubmitHandler
    {
        [Header("Fade")]
        [SerializeField]
        private float _Delay;
        [SerializeField]
        private Gradient _FadeGradient;
        [SerializeField]
        private Image _Panel;
        [SerializeField]
        private float _FadeInTime;
        [SerializeField]
        private float _FadeOutTime;

        [SerializeField]
        private float _ActiveTime;

        [Space]
        [SerializeField]
        UnityEvent _OnHideEvent;

        private Coroutine _Coroutine;

        private void OnEase(float easeValue)
        {
            _Panel.color = _FadeGradient.Evaluate(easeValue);
        }

        private void OnEaseOut()
        {
            _OnHideEvent?.Invoke();

            if(_Coroutine != null)
                StopCoroutine(_Coroutine);

            transform.SetActiveChildren(false);
        }

        private IEnumerator SplashScreenSequence()
        {
            yield return new WaitForSeconds(_Delay);

            yield return VFXUtilities.LinearEaseIn(OnEase, () => Time.deltaTime, null, _FadeInTime);

            yield return new WaitForSeconds(_ActiveTime);

            Hide();
        }

        public override void Hide()
        {
            if(!isShown)
                return;

            isShown = false;

            if(_Coroutine != null)
                StopCoroutine(_Coroutine);
                
            _Coroutine = StartCoroutine(VFXUtilities.LinearEaseOut(OnEase, () => Time.deltaTime, null, _FadeOutTime, OnEaseOut));
        }

        public override void Show()
        {
            base.Show();
            
            if(_Coroutine != null)
                StopCoroutine(_Coroutine);

            _Coroutine = StartCoroutine(SplashScreenSequence());
        }

        public void OnSubmit()
        {
            Hide();
        }
    }
}