using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;
using CDR.AttackSystem;
using CDR.TargetingSystem;
using CDR.MovementSystem;

namespace CDR.InputSystem
{
    public class AIMechInput : AIActiveCharacterInput<Mech>
    {
        private Vector3[] _DirectionsToAvoid = null;
        private Quaternion _WeightedAvoidanceRotation;
        private IActiveCharacter _CurrentTarget;

        private Vector3 _MoveDirection;

        protected override void Update()
        {
            base.Update();

            Vector3 dirAwayFromTarget = character.position - _CurrentTarget.position;
            Vector3 dirAwayFromEdge = character.position -  GetBoundaryEdge();

            _DirectionsToAvoid = Projectile.projectiles.Select(p => character.position - p.position).Concat(new Vector3[] { dirAwayFromEdge, dirAwayFromTarget }).ToArray();

            _WeightedAvoidanceRotation = GetWeightedDirectionAverageRotation(_DirectionsToAvoid);
        }

        private struct WeightedDirection
        {
            public Vector3 direction { get; }
            public float weight { get; }
            public float magnitude { get; }

            public WeightedDirection(Vector3 direction, float magnitude, float weight)
            {
                this.direction = direction;
                this.weight = weight;
                this.magnitude = magnitude;
            }

            public WeightedDirection(Vector3 direction, float weight) : this(direction.normalized, direction.magnitude, weight) { }

            public Quaternion GetRotation(Vector3 up)
            {
                return Quaternion.LookRotation(direction, up);
            }

            public Vector3 GetEulerAngles(Vector3 up)
            {
                return GetRotation(up).eulerAngles;
            }
        }

        private Quaternion GetWeightedDirectionAverageRotation(params Vector3[] directions)
        {
            if(directions.Length <= 0)
                return Quaternion.identity;

            if(directions.Length == 1)
            {
                WeightedDirection weightedDirection = new WeightedDirection(directions[0], 1);

                return weightedDirection.GetRotation(Vector3.up);
            }

            float longestMagnitude = directions.Max(d => d.magnitude);

            Quaternion weightedRotation = Quaternion.LookRotation(directions.First(), Vector3.up);

            foreach(WeightedDirection w in directions.Select(d => new WeightedDirection(d, 1 - (d.magnitude / longestMagnitude))).OrderByDescending(d => d.magnitude))
            {
                Quaternion rotation = w.GetRotation(Vector3.up);

                weightedRotation = Quaternion.Slerp(weightedRotation, rotation, w.weight);
            }

            return weightedRotation;
        }

        private float GetAsin(float value, float radius)
        {
            float sign = Mathf.Sign(value);

            return sign * Mathf.Min(Mathf.Asin(Mathf.Abs(value) / radius), radius);
        }

        private float GetPositiveDegree(float degree)
        {
            if(degree < 0)
                degree += 360;

            if(degree > 0)
                return degree;

            return GetPositiveDegree(degree);
        }

        private Vector3 GetBoundaryEdge()
        {
            IFlightPlane flightPlane = character.controller.flightPlane;
            
            Vector3 dirCenterToCharacter = (character.position - flightPlane.position).normalized;

            float xValue = Mathf.Rad2Deg * GetAsin(character.position.y - flightPlane.position.y, flightPlane.radius);
            float yValue = Quaternion.LookRotation(dirCenterToCharacter, Vector3.up).eulerAngles.y - 180;

            return Quaternion.Euler(xValue, yValue, 0) * -Vector3.forward * flightPlane.radius;
        }

        private void OnSwitchTarget(ITargetData targetData)
        {
            _CurrentTarget = targetData.activeCharacter;
        }

        private void OnBoost()
        {
            if(_CurrentTarget == null || _DirectionsToAvoid == null || character.boost.isActive)
                return;

            int objectsNear = _DirectionsToAvoid.Count(v => v.magnitude < 2);

            if(objectsNear >= 3)
            {
                character.boost.VerticalBoost(1);

                Debug.Log("[AI Input] Vertical Boost Up!");
            }

            else
            {
                Vector3 dirMove = Quaternion.Inverse(character.rotation) * _WeightedAvoidanceRotation * Vector3.forward;

                character.boost.HorizontalBoost(new Vector2(dirMove.x, dirMove.z));

                Debug.Log("[AI Input] Horizontal Boost Up!");
            } 
        }

        private void OnMove()
        {
            if(_CurrentTarget == null)
                return;

            Vector3 dirMove = Quaternion.Inverse(character.rotation) * _WeightedAvoidanceRotation * Vector3.forward;

            _MoveDirection = Vector3.Lerp(_MoveDirection, new Vector3(dirMove.x, dirMove.z, 0), Time.deltaTime / 2);

            character.movement.Move(_MoveDirection);
        }

        public override void SetupInput()
        {
            AddAction("Move", OnMove);
            AddAction("Boost", OnBoost);

            character.targetHandler.onSwitchTarget += OnSwitchTarget;
        }
    }
}