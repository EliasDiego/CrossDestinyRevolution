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

        private bool _IsAssignedInput = false;

        private bool _IsEnabled = false;

        private Dictionary<string, InputAction> _InputActions = new Dictionary<string, InputAction>();

        private bool _isGamepad = false;

        protected InputActionMap actionMap => _ActionMap;
        
        public InputDevice[] pairedDevices => _User.valid ? _User.pairedDevices.ToArray() : null;
        
        public bool isEnabled => _IsEnabled;

        public bool isAssignedInput => _IsAssignedInput;

        protected virtual void OnDestroy()
        {
            UnassignInput();
        }

        private bool IsUserNotNull(InputUser user)
        {
            if(user == null)
            {
                Debug.LogAssertion("[Input System Error] Input User has not yet been created!");

                return false;
            }

            return true;
        }

        private bool IsDeviceValid(InputDevice[] devices)
        {
            if(devices == null || devices.Length <= 0)
            {
                Debug.LogAssertion("[Input System Error] No Device(s) Assigned!");

                return false;
            }

            return true;
        }

        protected bool GetInputAction(string name, out InputAction inputAction)
        {
            if(_InputActions.ContainsKey(name))
            {
                inputAction = _InputActions[name];

                return true;
            }

            Debug.LogWarning($"[Input Error] {name} Input Action doesn't exist!");

            inputAction = null;

            return false;
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

        public void PairDevice(params InputDevice[] devices)
        {
            if(!IsUserNotNull(_User) || !IsDeviceValid(devices))
                return;

            _Gamepads = devices?.Where(d => d is Gamepad)?.Cast<Gamepad>()?.ToArray();

            _isGamepad = _Gamepads != null && _Gamepads.Length > 0;
            
            foreach(InputDevice device in devices)
                _User = InputUser.PerformPairingWithDevice(device, _User);
        }

        public void UnpairDevice(params InputDevice[] devices)
        {
            if(!IsUserNotNull(_User) || !_User.valid || !IsDeviceValid(devices))
                return;

            foreach(InputDevice device in devices)
                _User.UnpairDevice(device);
        }

        public virtual void AssociateActionMap(InputActionMap inputActionMap)
        {
            if(!_User.valid)
                return;

            _ActionMap = inputActionMap.Clone();

            _InputActions.Clear();

            foreach(InputAction inputAction in actionMap.actions)
                _InputActions.Add(inputAction.name, inputAction);

            _User.AssociateActionsWithUser(_ActionMap);
        }

        public void AssignInput(InputActionMap inputActionMap, params InputDevice[] devices)
        {
            if(isAssignedInput)
                UnassignInput();

            _User = default(InputUser);

            devices = devices?.Where(d => d != null)?.ToArray();

            PairDevice(devices);

            Debug.Assert(_User.valid, "[Input System Error] Input User is not valid!");

            AssociateActionMap(inputActionMap);

            _IsAssignedInput = true;
        }

        public void UnassignInput()
        {
            if(_User != null && _User.valid)
                _User.UnpairDevicesAndRemoveUser();

            _IsAssignedInput = true;
        }

        public virtual void EnableInput()
        {
            actionMap.Enable();

            _IsEnabled = true;
        }

        public virtual void DisableInput()
        {
            actionMap.Disable();
            
            _IsEnabled = false;
        }

        public void EnableInput(string name)
        {
            if(_InputActions.ContainsKey(name))
                _InputActions[name].Enable();

            else
                Debug.Log($"[Input Error] {name} does not exist!");

            _IsEnabled = _InputActions.Values.FirstOrDefault(i => i.enabled) != null;
        }

        public void DisableInput(string name)
        {
            if(_InputActions.ContainsKey(name))
                _InputActions[name].Disable();

            else
                Debug.Log($"[Input Error] {name} does not exist!");

            _IsEnabled = _InputActions.Values.FirstOrDefault(i => i.enabled) != null;
        }
    }
}