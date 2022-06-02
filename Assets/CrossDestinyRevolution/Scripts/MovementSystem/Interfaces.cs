using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.ActionSystem;

namespace CDR.MovementSystem
{
    public interface IBoostData
    {
        float time { get; }
        float distance { get; }
    }

    public interface IBoostValue : IValueRange
    {
        float regenerationRate { get; }
        float valueConsumption { get; }
    }

    public interface IController
    {
        Vector3 velocity { get; }

        void Translate(Vector3 direction, float magnitude);
        void Rotate(Quaternion rotation);
        void SetVelocity(Vector3 velocity);
    }

    public interface ICharacterController : IController
    {
        CDR.MovementSystem.IFlightPlane flightPlane { get; set; }
    }

    public interface IFlightPlane
    {
        Vector3 position { get; }
        Quaternion rotation { get; }
    }

    public interface IMovement : IAction
    {
        float speed { get; }
        float gravity { get; }

        void Move(Vector2 direction);
        void SetSpeedClamp(bool isClamped);
        void SetDistanceToTarget(float distance);
    }

    public interface IBoost : IAction
    {
        IBoostValue boostValue { get; }
        IBoostData horizontalBoostData { get; }
        IBoostData verticalBoostData { get; }
        
        void HorizontalBoost(Vector2 direction);
        void VerticalBoost(float direction);
    }
}