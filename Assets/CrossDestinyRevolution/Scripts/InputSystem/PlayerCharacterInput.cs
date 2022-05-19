using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

using CDR.MechSystem;

namespace CDR.InputSystem
{
    public class PlayerCharacterInput<T> : CharacterInput<T>, IPlayerInput where T : ActiveCharacter
    {
        private InputUser _User;
        private InputDevice _Device;
        private InputActionAsset _ActionAsset;

        public InputUser user => _User;
        public InputDevice device => _Device;
        public InputActionAsset inputActionAsset => _ActionAsset;

        protected virtual void OnDestroy()
        {
            if(user != null && user.valid)
                user.UnpairDevicesAndRemoveUser();
        }

        public void SetupInput(InputActionAsset inputActionAsset, InputDevice device)
        {
            _Device = device;
            
            _User = InputUser.PerformPairingWithDevice(device);

            Debug.Assert(_User.valid, "[Input System Error] Input User is not valid!");

            _ActionAsset = Instantiate(inputActionAsset);

            _User.AssociateActionsWithUser(_ActionAsset);
        }

        public virtual void EnableInput()
        {
            if(inputActionAsset)
                inputActionAsset.Enable();
        }

        public virtual void DisableInput()
        {
            if(inputActionAsset)
                inputActionAsset.Disable();
        }
    }
}