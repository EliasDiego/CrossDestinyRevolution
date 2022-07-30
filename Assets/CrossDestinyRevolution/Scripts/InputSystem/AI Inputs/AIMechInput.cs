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

        private Vector3 _DebugPosition;
        private Vector3 _DebugDirection;

        private void OnDrawGizmos() 
        {
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

        private float GetAsin(float value, float radius)
        {
            float sign = Mathf.Sign(value);

            return sign * Mathf.Min(Mathf.Asin(Mathf.Abs(value) / radius), radius);
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

            Vector3 dirAwayFromTarget = _CurrentTarget.position - character.position;
            Vector3 dirAwayFromEdge = GetBoundaryEdge() - character.position;

            Quaternion rotAwayFromTarget = Quaternion.LookRotation(character.rotation * dirAwayFromTarget, Vector3.up);
            Quaternion rotAwayFromEdge = Quaternion.LookRotation(character.rotation * dirAwayFromTarget, Vector3.up);

            Vector3 dirMove = Quaternion.Lerp(rotAwayFromEdge, rotAwayFromTarget, 0.5f) * -Vector3.forward;
            // Vector3 dirMove = new Vector3(1, 0, 0);
            
            character.movement.Move(new Vector2(dirMove.x, dirMove.z));

            _DebugPosition = character.position;
            _DebugDirection = dirMove * 10;
        }

        public override void SetupInput()
        {
            AddAction("Move", OnMove);

            character.targetHandler.onSwitchTarget += OnSwitchTarget;
        }
    }
}