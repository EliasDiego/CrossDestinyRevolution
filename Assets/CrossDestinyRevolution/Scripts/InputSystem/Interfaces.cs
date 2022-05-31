using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

using CDR;

namespace CDR.InputSystem
{
    public interface IInput
    {
        void EnableInput();
        void DisableInput();
    }

    public interface IPlayerInput : IInput
    {
        public InputUser user { get; }
        void SetupInput(InputActionMap inputActionMap, params InputDevice[] devices);
        void EnableInput(string name);
        void DisableInput(string name);
    }

    public interface IAIInput : IInput
    {
        void SetupInput();
    }

    public interface IMinMaxRange
    {
        float minValue { get; }
        float maxValue { get; }

        bool IsWithinRange(float value);
    }

    public interface IBoostInputSettings
    {
        float movementInputThreshold { get; }
        IMinMaxRange boostUpHeightRange { get; }
        float boostDownMinHeight { get; }
    }

    public interface IPlayerInputSettings
    {
        
    }

    public interface IPlayerMechInputSettings : IPlayerInputSettings
    {
        IBoostInputSettings boostInputSettings { get; }
    }

    public interface IPlayerSubmitHandler
    {
        void OnPlayerSubmit(IPlayerInput playerInput);
    }

    public interface IPlayerCancelHandler
    {
        void OnPlayerCancel(IPlayerInput playerInput);
    }
}