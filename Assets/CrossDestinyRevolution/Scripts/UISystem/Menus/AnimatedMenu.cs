using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.UISystem
{
    [RequireComponent(typeof(Animation))]
    public class AnimatedMenu : Menu
    {
        [Header("Animation")]
        [SerializeField]
        private AnimationClip _ShowAnimationClip;
        [SerializeField]
        private AnimationClip _HideAnimationClip;

        private Animation _Animation;

        private Coroutine _Coroutine;

        protected virtual void Awake() 
        {
            _Animation = GetComponent<Animation>();
        }

        protected virtual IEnumerator ShowAnimatedSequence()
        {
            _OnShow?.Invoke();
            transform.SetActiveChildren(true);

            if(!_ShowAnimationClip)
                yield break;

            _Animation.Play(_ShowAnimationClip.name);
            
            AnimationState state = _Animation[_ShowAnimationClip.name];

            if(Time.timeScale > 0)
                yield return new WaitWhile(() => _Animation.isPlaying);

            else while(_Animation.isPlaying)
            {
                state.time += Time.unscaledDeltaTime;
                _Animation.Sample();

                yield return null;
            }

            isShown = true;
        }

        protected virtual IEnumerator HideAnimatedSequence()
        {
            if(_HideAnimationClip)
            {
                _Animation.Play(_HideAnimationClip.name);

                AnimationState state = _Animation[_HideAnimationClip.name];

                if(Time.timeScale > 0)
                    yield return new WaitWhile(() => _Animation.isPlaying);

                else while(_Animation.isPlaying)
                {
                    state.time += Time.unscaledDeltaTime;
                    _Animation.Sample();

                    yield return null;
                }
            }
            
            base.Hide();
        }

        public override void Show()
        {
            if(_Coroutine != null)
                StopCoroutine(_Coroutine);

            _Coroutine = StartCoroutine(ShowAnimatedSequence());
        }

        public override void Hide()
        {
            if(_Coroutine != null)
                StopCoroutine(_Coroutine);

            _Coroutine = StartCoroutine(HideAnimatedSequence());
        }
    }
}