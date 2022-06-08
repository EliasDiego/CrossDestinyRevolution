using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.InputSystem;

namespace CDR.UISystem
{
    public class CreditsMenu : Menu, IMenuCancelHandler
    {
        public void OnCancel()
        {
            Back();
        }
    }
}