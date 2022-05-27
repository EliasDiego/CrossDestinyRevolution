using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace CDR.InputSystem
{
    public class PlayerUIInput : MonoBehaviour, IPlayerInput
    {
        public InputUser user => throw new System.NotImplementedException();

        public void DisableInput(string name)
        {
            throw new System.NotImplementedException();
        }

        public void DisableInput()
        {
            throw new System.NotImplementedException();
        }

        public void EnableInput(string name)
        {
            throw new System.NotImplementedException();
        }

        public void EnableInput()
        {
            throw new System.NotImplementedException();
        }

        public void SetupInput(InputActionMap inputActionMap, params InputDevice[] devices)
        {
            throw new System.NotImplementedException();
        }
    }
}