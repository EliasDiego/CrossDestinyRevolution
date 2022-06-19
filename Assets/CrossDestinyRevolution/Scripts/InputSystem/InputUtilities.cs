using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.LowLevel;

namespace CDR.InputSystem
{
    [System.Serializable]
    public static class InputUtilities
    {
        static InputUtilities()
        {
            InputUser.onUnpairedDeviceUsed += (control, ptr) => onUnpairedInputDevicUsedEvent?.Invoke(control.device);
            // UnityEngine.InputSystem.InputSystem.onAnyButtonPress
        }

        public static event Action<InputDevice> onUnpairedInputDevicUsedEvent;

        public static InputDevice[] GetAllInputDevices()
        {
            return UnityEngine.InputSystem.InputSystem.devices.ToArray();
        }

        public static InputDevice[] GetAllInputDevices(params InputDevice[] excludedDevices)
        {
            return GetAllInputDevices()?.Except(excludedDevices)?.ToArray();
        }

        public static InputDevice[] GetAllUnpairedInputDevices()
        {
            return InputUser.GetUnpairedInputDevices().ToArray();
        }

        public static InputDevice[] GetAllUnpairedInputDevices(params InputDevice[] excludedDevices)
        {
            return GetAllUnpairedInputDevices()?.Except(excludedDevices)?.ToArray();
        }

        public static TInput AssignPlayerInput<TInput>(GameObject gameObject, InputActionMap inputActionMap, params InputDevice[] devices) 
            where TInput : MonoBehaviour, IPlayerInput
        {
            Debug.Assert(inputActionMap != null, $"[Input System Error] Input Action Map is not valid!");

            TInput playerInput = gameObject.AddComponent<TInput>();

            playerInput.AssignInput(inputActionMap, devices);

            return playerInput;
        }

        public static T AssignAIInput<T>(GameObject gameObject) where T : MonoBehaviour, IAIInput
        {
            T aIInput = gameObject.AddComponent<T>();

            aIInput.SetupInput();

            return aIInput;
        }
    }
}