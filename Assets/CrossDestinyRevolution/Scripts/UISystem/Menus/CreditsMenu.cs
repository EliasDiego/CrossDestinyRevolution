using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.InputSystem;

namespace CDR.UISystem
{
    public class CreditsMenu : Menu, IMenuCancelHandler
    {
        [SerializeField]
        Transition _CreditsTransition;

        public void OnCancel()
        {
            _CreditsTransition.Back();
        }
    }
}