using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

using CDR.MechSystem;

namespace CDR.InputSystem
{
    public class PlayerCharacterInput<T> : CharacterInput<T>, IPlayerInput where T : IActiveCharacter
    {
        private InputUser _User;
        private InputActionAsset _ActionAsset;

        public InputUser user => _User;
        public InputActionAsset inputActionAsset => _ActionAsset;

        protected virtual void OnDestroy()
        {
            if(user != null && user.valid)
                user.UnpairDevicesAndRemoveUser();
        }

        public void SetupInput(InputActionAsset inputActionAsset, params InputDevice[] devices)
        {
            _User = default(InputUser);
            
            foreach(InputDevice device in devices.Where(d => d != null))
                _User = InputUser.PerformPairingWithDevice(device, _User);

            Debug.Assert(_User.valid, "[Input System Error] Input User is not valid!");

            _ActionAsset = Instantiate(inputActionAsset);

            _User.AssociateActionsWithUser(_ActionAsset);
        }

        public override void EnableInput()
        {
            if(inputActionAsset)
                inputActionAsset.Enable();
        }

        public override void DisableInput()
        {
            if(inputActionAsset)
                inputActionAsset.Disable();
        }
    }
}