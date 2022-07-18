using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.InputSystem;

namespace CDR.UISystem
{
    public class CreditsMenu : AnimatedMenu, IMenuCancelHandler
    {
        [SerializeField]
        Transition _CreditsTransition;

        public void OnCancel()
        {
            Debug.Log("Blah");
            _CreditsTransition.Back();
        }
    }
}