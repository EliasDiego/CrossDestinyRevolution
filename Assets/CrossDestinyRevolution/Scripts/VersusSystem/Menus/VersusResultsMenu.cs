using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.UISystem;

namespace CDR.VersusSystem
{
    public class VersusResultsMenu : Menu, IVersusResultsMenu
    {
        public event Action rematchEvent;
        public event Action returnToMainMenuEvent;

        public void InvokRematch()
        {
            rematchEvent?.Invoke();
        }

        public void InvokeReturnToMainMenu()
        {
            returnToMainMenuEvent?.Invoke();
        }
    }
}