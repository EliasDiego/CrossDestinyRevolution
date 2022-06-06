using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.UISystem
{
    public class AnimatedMenu : Menu
    {
        private Coroutine _ShowCoroutine;
        private Coroutine _HideCoroutine;
        
        private IEnumerator ShowCoroutine()
        {
            yield return null;
        }

        private IEnumerator HideCoroutine()
        {
            
            yield return null;
        }

        public override void Hide()
        {
            if(_ShowCoroutine != null)
                StopCoroutine(_ShowCoroutine);

            _HideCoroutine = StartCoroutine(HideCoroutine());
        }

        public override void Show()
        {
            if(_HideCoroutine != null)
                StopCoroutine(_HideCoroutine);
                
            _ShowCoroutine = StartCoroutine(ShowCoroutine());
        }
    }
}