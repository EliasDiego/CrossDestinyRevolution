using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace CDR.InputSystem
{
    public class PlayerInput : MonoBehaviour, IPlayerInput
    {
        private InputUser _User;
        private InputActionMap _ActionMap;
        private Gamepad[] _Gamepads;

        private Dictionary<string, InputAction> _InputActions = new Dictionary<string, InputAction>();

        private bool _isGamepad = false;

        protected InputActionMap actionMap => _ActionMap;
        protected Dictionary<string, InputAction> inputActions => _InputActions;
        
        public InputUser user => _User;

        protected virtual void OnDestroy()
        {
            if(user != null && user.valid)
                user.UnpairDevicesAndRemoveUser();
        }

        protected void StartHaptic(float lowFrequency, float highFrequency)
        {
            if(!_isGamepad)
                return;

            foreach(Gamepad gamepad in _Gamepads)
                gamepad.SetMotorSpeeds(lowFrequency, highFrequency);
        }

        protected void StopHaptic()
        {
            if(!_isGamepad)
                return;

            foreach(Gamepad gamepad in _Gamepads)
                gamepad.ResetHaptics();
        }

        public virtual void SetupInput(InputActionMap inputActionMap, params InputDevice[] devices)
        {
            _User = default(InputUser);

            devices = devices?.Where(d => d != null)?.ToArray();

            if(devices == null || devices.Length <= 0)
            {
                Debug.LogAssertion("[Input System Error] No Device(s) Assigned!");

                return;
            }

            _Gamepads = devices?.Where(d => d is Gamepad)?.Cast<Gamepad>()?.ToArray();

            _isGamepad = _Gamepads != null && _Gamepads.Length > 0;
            
            foreach(InputDevice device in devices)
                _User = InputUser.PerformPairingWithDevice(device, _User);

            Debug.Assert(_User.valid, "[Input System Error] Input User is not valid!");

            _ActionMap = inputActionMap.Clone();

            foreach(InputAction inputAction in actionMap.actions)
                _InputActions.Add(inputAction.name, inputAction);

            _User.AssociateActionsWithUser(_ActionMap);
        }

        public virtual void EnableInput()
        {
            actionMap.Enable();
        }

        public virtual void DisableInput()
        {
            actionMap.Disable();
        }

        public void EnableInput(string name)
        {
            if(_InputActions.ContainsKey(name))
                _InputActions[name].Enable();

            else
                Debug.Log($"[Input Error] {name} does not exist!");
        }

        public void DisableInput(string name)
        {
            if(_InputActions.ContainsKey(name))
                _InputActions[name].Disable();

            else
                Debug.Log($"[Input Error] {name} does not exist!");
        }
    }
}