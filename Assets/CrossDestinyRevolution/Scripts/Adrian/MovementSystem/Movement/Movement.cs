using System;
using UnityEngine;
using CDR.ActionSystem;

// This base class handles movement of an entity.

namespace CDR.MovementSystem
{
    public class Movement 
    {
        public float Speed { get; protected set; }
        public bool CanMove { get; protected set; } = true;

        public float movingTime = 0f;
        protected Transform transformToMove;

        // CONSTRUTOR
        public Movement(float speed, Transform trans)
        {
            Speed = speed;
            transformToMove = trans;
        }

        // MOVEMENT METHODS
        public virtual void Move(Vector3 direction) 
        {
            transformToMove.position += direction * Speed * Time.deltaTime;
        }

        // MODIFIERS
        public void SetSpeed(float value)
        {
            Speed = value;
        }

        // GETTERS & SETTERS
        public void SetCanMove(bool boo)
        {
            CanMove = boo;
        }

        public virtual bool IsMoving()
        {
            return Speed != 0f;
        }
    }
}
