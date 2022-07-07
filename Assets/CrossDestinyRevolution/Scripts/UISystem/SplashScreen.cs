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

        private IEnumerator SplashScreenSequence()
        {
            yield return new WaitForSeconds(2);

            yield return VFXUtilities.LinearEaseIn(OnEase, () => Time.deltaTime, null, _FadeInTime);

            yield return new WaitForSeconds(_ActiveTime);

            Hide();
        }

        public override void Hide()
        {
            base.Hide();

            if(_Coroutine != null)
                StopCoroutine(_Coroutine);
                
            _Coroutine = StartCoroutine(VFXUtilities.LinearEaseIn(OnEase, () => Time.deltaTime, null, _FadeOutTime, () => _OnHideEvent?.Invoke()));
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
            if(isShown)
                Hide();
        }
    }
}