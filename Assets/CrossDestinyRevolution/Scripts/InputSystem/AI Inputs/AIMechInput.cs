using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;
using CDR.TargetingSystem;
using CDR.MovementSystem;

namespace CDR.InputSystem
{
    public class AIMechInput : AIActiveCharacterInput<Mech>
    {   
        private IActiveCharacter _CurrentTarget;

        private Vector3[] _DebugPositions;
        private Vector3 _DebugPosition;
        private Vector3 _DebugDirection;

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

        private void OnDrawGizmos() 
        {
            for(int i = 0; i < _DebugPositions.Length; i++)
            {
                Gizmos.DrawSphere(_DebugPositions[i], 5);
            }

            if(_DebugDirection == Vector3.zero)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(_DebugPosition, 10);
            }

            else
            {
                Gizmos.DrawLine(_DebugPosition, _DebugPosition + _DebugDirection);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(_DebugPosition + _DebugDirection, 10);
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

            string debug = "";

            float longestMagnitude = directions.Select(d => d.magnitude).Max();

            Quaternion weightedRotation = Quaternion.identity;

            foreach(WeightedDirection w in directions.Select(d => new WeightedDirection(d, 1 - (d.magnitude / longestMagnitude))).OrderByDescending(d => d.magnitude))
            {
                Quaternion rotation = w.GetRotation(Vector3.up);

                weightedRotation = Quaternion.Slerp(weightedRotation, rotation, .5f);

                debug += rotation.eulerAngles.y + "\n";
            }

            Debug.Log(weightedRotation.eulerAngles.y + "\n\n" + debug);

            return weightedRotation; //Quaternion.Euler(weightedRotation.eulerAngles.x, weightedRotation.eulerAngles.y + 180, weightedRotation.eulerAngles.z);
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

        private void OnMove()
        {
            if(_CurrentTarget == null)
                return;

            Vector3 boundaryEdge = GetBoundaryEdge();

            Vector3 dirAwayFromTarget = character.position - _CurrentTarget.position;
            Vector3 dirAwayFromEdge = character.position - boundaryEdge;

            Quaternion weightedRotation = GetWeightedDirectionAverageRotation(dirAwayFromTarget, dirAwayFromEdge);

            Vector3 dirMove = weightedRotation * Vector3.forward;

            character.controller.Translate(new Vector3(dirMove.x, 0, dirMove.z), .1f);

            _DebugPositions[0] = boundaryEdge;
            _DebugPosition = character.position;
            _DebugDirection = dirMove * 30;
        }

        public override void SetupInput()
        {
            _DebugPositions = new Vector3[5];

            AddAction("Move", OnMove);

            character.targetHandler.onSwitchTarget += OnSwitchTarget;
        }
    }
}