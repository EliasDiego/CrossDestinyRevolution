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
        private IActiveCharacter _CurrentTarget;

        private Vector3 _MoveDirection;

        public AIMechInputSettings settings { get; set; }

        protected override void Update()
        {
            base.Update();
            
            if(character.currentState != null)
                return;

            if(_AIActions?.Values?.Where(a => a.enabled).Count() <= 0)
            {
                Debug.LogError($"Paused because {character} has no inputs!\n" +
                    $"Movement: {character.movement.isActive}\n" +
                    $"Boost: {character.boost.isActive}\n" +
                    $"RangeAttack: {character.rangeAttack.isActive}\n" +
                    $"MeleeAttack: {character.movement.isActive} {character.meleeAttack.isCoolingDown}\n" +
                    $"Shield: {character.shield.isActive}\n" +
                    $"Special Attack 1: {character.specialAttack1.isActive}\n" +
                    $"Special Attack 2: {character.specialAttack2.isActive}\n" +
                    $"Special Attack 3: {character.specialAttack3.isActive}\n"
                    );

                // UnityEditor.EditorApplication.isPaused = true;

                return;
            }
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
            if(_CurrentTarget == null || character.boost.isActive)
                return;

            Vector3 dirAwayFromTarget = character.position - _CurrentTarget.position;
            Vector3 dirAwayFromEdge = character.position - GetBoundaryEdge();

            Vector3[] directionsToAvoid = Projectile.projectiles.Where(p => p.owner != (IActiveCharacter)character)?.Select(p => character.position - p.position)?.Concat(new Vector3[] { dirAwayFromEdge, dirAwayFromTarget })?.ToArray();

            Quaternion weightedAvoidanceRotation = GetWeightedDirectionAverageRotation(directionsToAvoid);

            int objectsNear = directionsToAvoid.Count(v => v.magnitude < 2);

            if(objectsNear >= 3)
            {
                character.boost.VerticalBoost(1);

                Debug.Log($"[{character} AI Input] Vertical Boost Up!");
            }

            else if(objectsNear > 0)
            {
                Vector3 dirMove = Quaternion.Inverse(character.rotation) * weightedAvoidanceRotation * Vector3.forward;

                character.boost.HorizontalBoost(new Vector2(dirMove.x, dirMove.z));

                Debug.Log($"[{character} AI Input] Horizontal Boost!");
            } 
        }

        private void OnMove()
        {
            if(_CurrentTarget == null)
                return;

            Vector3 dirAwayFromTarget = character.position - _CurrentTarget.position;
            Vector3 dirAwayFromEdge = character.position - GetBoundaryEdge();

            Vector3[] directionsToAvoid = Projectile.projectiles.Where(p => p.owner != (IActiveCharacter)character)?.Select(p => character.position - p.position).Concat(new Vector3[] { dirAwayFromEdge, dirAwayFromTarget }).ToArray();

            Quaternion weightedAvoidanceRotation = GetWeightedDirectionAverageRotation(directionsToAvoid);

            Vector3 dirMove = Quaternion.Inverse(character.rotation) * weightedAvoidanceRotation * Vector3.forward;

            _MoveDirection = Vector3.Lerp(_MoveDirection, new Vector3(dirMove.x, dirMove.z, 0), Time.deltaTime / 2);

            character.movement.Move(_MoveDirection);

            Debug.Log($"[{character} AI Input] Movement!");
        }

        private void OnRangeAttack()
        {
            if(_CurrentTarget == null || character.rangeAttack.isActive)
                return;

            character.rangeAttack.Use();

            Debug.Log($"[{character} AI Input] Range Attack!");
        }

        private void OnMeleeAttack()
        {
            if(_CurrentTarget == null || character.meleeAttack.isCoolingDown)
                return;

            if(_CurrentTarget is Mech && (_CurrentTarget as Mech).meleeAttack.isActive)
                return;

            if((_CurrentTarget.position - character.position).magnitude < 50)
            {
                if(!character.meleeAttack.isActive)
                {
                    character.meleeAttack.Use();

                    Debug.Log($"[{character} AI Input] Melee Attack!");
                }
            }

            else if(character.meleeAttack.isActive)
                character.meleeAttack.End();
        }

        private void OnShield()
        {
            if(_CurrentTarget == null)
                return;

            if(!(_CurrentTarget is Mech) || !(_CurrentTarget as Mech).meleeAttack.isActive)
            {
                if(character.shield.isActive)
                    character.shield.End();

                return;
            }
                
            if((_CurrentTarget.position - character.position).magnitude < 30)
            {
                if(!character.shield.isActive)
                {
                    character.shield.Use();

                    Debug.Log($"[{character} AI Input] Shield!");
                }
            }
                    
            else if(character.shield.isActive)
                character.shield.End();
        }

        private void OnSpecialAttack1()
        {
            if(_CurrentTarget == null || !settings || character.specialAttack1.isCoolingDown)
                return;

            if(settings.specialAttack1Range.IsWithinRange((_CurrentTarget.position - character.position).magnitude))
            {
                if(!character.specialAttack1.isActive)
                {
                    character.specialAttack1.Use();

                    Debug.Log($"[{character} AI Input] Special Attack 1!");
                }
            }
        }

        private void OnSpecialAttack2()
        {
            if(_CurrentTarget == null || !settings || character.specialAttack2.isCoolingDown)
                return;

            if(settings.specialAttack2Range.IsWithinRange((_CurrentTarget.position - character.position).magnitude))
            {
                if(!character.specialAttack2.isActive)
                {
                    character.specialAttack2.Use();

                    Debug.Log($"[{character} AI Input] Special Attack 2!");
                }
            }
        }
        
        private void OnSpecialAttack3()
        {
            if(_CurrentTarget == null || !settings || character.specialAttack3.isCoolingDown)
                return;

            if(settings.specialAttack3Range.IsWithinRange((_CurrentTarget.position - character.position).magnitude))
            {
                if(!character.specialAttack3.isActive)
                {
                    character.specialAttack3.Use();

                    Debug.Log($"[{character} AI Input] Special Attack 3!");
                }
            }
        }

        public override void SetupInput()
        {
            AddAction("Move", OnMove);
            AddAction("Boost", OnBoost);
            AddAction("RangeAttack", OnRangeAttack);
            AddAction("MeleeAttack", OnMeleeAttack);
            AddAction("Shield", OnShield);
            AddAction("SpecialAttack1", OnSpecialAttack1);
            AddAction("SpecialAttack2", OnSpecialAttack2);
            AddAction("SpecialAttack3", OnSpecialAttack3);

            character.targetHandler.onSwitchTarget += OnSwitchTarget;
        }
    }
}