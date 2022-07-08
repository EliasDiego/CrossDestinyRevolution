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
            base.Show();

            if(!_ShowAnimationClip)
                yield break;

            _Animation.Play(_ShowAnimationClip.name);

            yield return new WaitWhile(() => _Animation.isPlaying);
        }

        protected virtual IEnumerator HideAnimatedSequence()
        {
            if(_HideAnimationClip)
            {
                Debug.Log(_Animation + " " + _HideAnimationClip);
                _Animation.Play(_HideAnimationClip.name);

                yield return new WaitWhile(() => _Animation.isPlaying);
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