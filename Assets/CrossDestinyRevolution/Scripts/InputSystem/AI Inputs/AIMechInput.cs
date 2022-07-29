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

        private void OnSwitchTarget(ITargetData targetData)
        {
            _CurrentTarget = targetData.activeCharacter;
        }

        private void OnMove()
        {
            if(_CurrentTarget == null)
                return;

            IFlightPlane flightPlane = character.controller.flightPlane;

            Vector3 dirAwayFromTarget = (_CurrentTarget.position - character.position).normalized;
            Vector3 dirCenterToCharacter = (character.position - flightPlane.position).normalized;
            Vector3 posBoundaryEnd = flightPlane.position + dirCenterToCharacter * flightPlane.radius;

            _DebugPosition = posBoundaryEnd;
            // _DebugDirection = dirCenterToCharacter;
        }

        public override void SetupInput()
        {
            AddAction("Move", OnMove);

            character.targetHandler.onSwitchTarget += OnSwitchTarget;
        }
    }
}