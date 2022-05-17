using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace CDR.InputSystem
{
    public interface IInput
    {
        void EnableInput();
        void DisableInput();
    }

    public interface IPlayerInput : IInput
    {
        InputDevice device { get; }
        InputUser user { get; }
        InputActionAsset inputActionAsset { get; }
        void SetupInput(InputActionAsset inputActionAsset, InputDevice device);
    }

    public interface IAIInput : IInput
    {
        void SetupInput();
    }
}