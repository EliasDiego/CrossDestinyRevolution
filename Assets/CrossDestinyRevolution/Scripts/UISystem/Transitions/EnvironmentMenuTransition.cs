using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.UISystem
{
    public class EnvironmentMenuTransition : Transition
    {
        [SerializeField]
        private Menu _NextMenu;
        [SerializeField]
        GameObject _PreviousEnvironment;
        [SerializeField]
        GameObject _NextEnvironment;

        protected override IUIElement nextMenu => _NextMenu;

        protected override IEnumerator ShowAnimatedSequence()
        {
            yield return base.ShowAnimatedSequence();
            
            GameObject environment = isGoingNext ? _PreviousEnvironment : _NextEnvironment;
            
            environment.SetActive(false);
        }

        protected override IEnumerator HideAnimatedSequence()
        {
            GameObject environment = isGoingNext ? _NextEnvironment : _PreviousEnvironment;
            
            environment.SetActive(true);

            return base.HideAnimatedSequence();
        }
    }
}