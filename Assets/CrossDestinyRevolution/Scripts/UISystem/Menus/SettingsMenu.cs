using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.InputSystem;

namespace CDR.UISystem
{
    public class SettingsMenu : Menu, IMenuCancelHandler
    {
        public void OnCancel()
        {
            Back();
        }
    }
}