using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using CDR.MechSystem;

namespace CDR.InputSystem
{
    public class PlayerMechInput : PlayerCharacterInput<Mech>
    {
        private void OnMovement(InputAction.CallbackContext context)
        {
            Debug.Log("Blah");
        }

        public override void SetupInput(InputActionAsset inputActionAsset, InputDevice device)
        {
            base.SetupInput(inputActionAsset, device);

            this.inputActionAsset.Enable();

            Debug.Log(this.inputActionAsset.FindAction("Movement"));

            this.inputActionAsset.FindAction("Movement").started += OnMovement;
        }
    }
}