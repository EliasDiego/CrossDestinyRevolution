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
        bool isEnabled { get; }
        void EnableInput();
        void DisableInput();
        void EnableInput(string name);
        void DisableInput(string name);
    }

    public interface IPlayerInput : IInput
    {
        bool isAssignedInput { get; }
        InputDevice[] pairedDevices { get; }

        void PairDevice(params InputDevice[] devices);
        void UnpairDevice(params InputDevice[] devices);
        void AssociateActionMap(InputActionMap inputActionMap);
        void AssignInput(InputActionMap inputActionMap, params InputDevice[] devices);
        void UnassignInput();
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