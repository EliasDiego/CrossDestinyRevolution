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

        public override void EnableInput()
        {
            base.EnableInput();

            this.inputActionAsset.FindAction("Movement").started += OnMovement;
        }
    }
}