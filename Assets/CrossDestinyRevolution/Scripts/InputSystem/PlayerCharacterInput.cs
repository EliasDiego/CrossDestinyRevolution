using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

using CDR.MechSystem;
using System;

namespace CDR.InputSystem
{
    public abstract class PlayerCharacterInput<TCharacter, TSettings> : CharacterInput<TCharacter>, IPlayerInput<TSettings> 
        where TCharacter : IActiveCharacter
        where TSettings : IPlayerInputSettings
    {
        private InputUser _User;
        private InputActionAsset _ActionAsset;
        private TSettings _PlayerInputSettings;

        protected InputActionAsset actionAsset => _ActionAsset;
        protected TSettings settings => _PlayerInputSettings;

        public InputUser user => _User;

        protected virtual void OnDestroy()
        {
            if(user != null && user.valid)
                user.UnpairDevicesAndRemoveUser();
        }

        public virtual void SetupInput(TSettings playerInputSettings, InputActionAsset inputActionAsset, params InputDevice[] devices)
        {
            _User = default(InputUser);
            
            foreach(InputDevice device in devices.Where(d => d != null))
                _User = InputUser.PerformPairingWithDevice(device, _User);

            Debug.Assert(_User.valid, "[Input System Error] Input User is not valid!");

            _ActionAsset = Instantiate(inputActionAsset);

            _PlayerInputSettings = playerInputSettings;

            _User.AssociateActionsWithUser(_ActionAsset);
        }

        public override void EnableInput()
        {
            if(actionAsset)
                actionAsset.Enable();
        }

        public override void DisableInput()
        {
            if(actionAsset)
                actionAsset.Disable();
        }

        public abstract void EnableInput(string name);

        public abstract void DisableInput(string name);
    }
}