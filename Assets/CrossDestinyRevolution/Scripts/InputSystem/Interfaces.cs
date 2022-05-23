using System;
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
        InputUser user { get; }
        InputActionAsset actionAsset { get; }
        void SetupInput(InputActionAsset inputActionAsset, params InputDevice[] devices);
        void EnableInput(string name);
        void DisableInput(string name);
    }

    public interface IAIInput : IInput
    {
        void SetupInput();
    }
}