using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

namespace CDR.UISystem
{
    [RequireComponent(typeof(Animation))]
    public abstract class Transition : MonoBehaviour, IUIElement
    {
        [SerializeField]
        private float _TransitionInDelay;
        [SerializeField]
        private float _TransitionOutDelay;
        
        [Header("Animation")]
        [SerializeField]
        private AnimationClip _ShowAnimationClip;
        [SerializeField]
        private AnimationClip _HideAnimationClip;

        private IUIElement _PreviousUIElement;

        private Animation _Animation;

        private Coroutine _AnimationCoroutine;
        private Coroutine _TransitionCoroutine;

        private bool _IsGoingNext = false;

        protected bool isGoingNext => _IsGoingNext;

        protected abstract IUIElement nextMenu { get; }

        public bool isShown { get; protected set; } = false;

        protected virtual void Awake() 
        {
            _Animation = GetComponent<Animation>();
            
            transform.SetActiveChildren(false);
        }

        private IEnumerator TransitionSequence(IUIElement previousUIElement, IUIElement nextUIElement)
        {
            previousUIElement.Hide();

            yield return new WaitForSecondsRealtime(_TransitionInDelay);

            Show();

            yield return new WaitWhile(() => !isShown || previousUIElement.isShown);

            nextUIElement.Show();

            yield return new WaitForSecondsRealtime(_TransitionOutDelay);

            Hide();
        }

        protected virtual IEnumerator ShowAnimatedSequence()
        {
            transform.SetActiveChildren(true);

            if(!_ShowAnimationClip)
                yield break;

            _Animation.Play(_ShowAnimationClip.name);

            AnimationState state = _Animation[_ShowAnimationClip.name];

            if(Time.timeScale > 0)
                yield return new WaitWhile(() => _Animation.isPlaying);

            else while(_Animation.isPlaying)
            {
                Debug.Log("Show Blah");
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

                else 
                {
                    while(_Animation.isPlaying)
                    {
                        Debug.Log("Blah");
                        state.time += Time.unscaledDeltaTime;
                        _Animation.Sample();

                        yield return null;
                    }
                }
            }
            
            isShown = false;

            transform.SetActiveChildren(false);
        }

        public void Show()
        {
            if(_AnimationCoroutine != null)
                StopCoroutine(_AnimationCoroutine);

            _AnimationCoroutine = StartCoroutine(ShowAnimatedSequence());
        }

        public void Hide()
        {
            if(_AnimationCoroutine != null)
                StopCoroutine(_AnimationCoroutine);

            _AnimationCoroutine = StartCoroutine(HideAnimatedSequence());
        }

        public void Next(IUIElement currentUIElement)
        {
            if(_TransitionCoroutine != null)
                StopCoroutine(_TransitionCoroutine);

            _PreviousUIElement = currentUIElement;

            _IsGoingNext = true;

            _TransitionCoroutine = StartCoroutine(TransitionSequence(_PreviousUIElement, nextMenu));
        }

        public void Back()
        {
            if(_TransitionCoroutine != null)
                StopCoroutine(_TransitionCoroutine);

            _IsGoingNext = false;

            _TransitionCoroutine = StartCoroutine(TransitionSequence(nextMenu, _PreviousUIElement));

            _PreviousUIElement = null;
        }
    }
}