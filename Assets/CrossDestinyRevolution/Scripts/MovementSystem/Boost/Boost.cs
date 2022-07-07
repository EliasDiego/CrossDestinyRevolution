using System.Collections;
using UnityEngine;
using CDR.AnimationSystem;
using CDR.VFXSystem;

// This class is for the Boost system and its methods.

namespace CDR.MovementSystem
{
    public class Boost : ActionSystem.Action, IBoost
    {
        [Tooltip("Delay before resuming boost regen in seconds.")]
        [SerializeField]
        private float regenDelaySeconds;
        [SerializeField]
        private float stopMinDistance = 6f;
        [SerializeField]
        private BoostValue _boostValue;
        [SerializeField]
        private VerticalBoostData _verticalBoostData;
        [SerializeField]
        private HorizontalBoostData _horizontalBoostData;
        [SerializeField]
        private AnimationCurve animationCurve;
        [SerializeField]
        private BoostVFXHandler[] vfxHandler;
        [SerializeField]
        private SFXAnimationEvent[] sfx;

        public IBoostValue boostValue => _boostValue;
        public IBoostData horizontalBoostData => _horizontalBoostData;
        public IBoostData verticalBoostData => _verticalBoostData;

        private Coroutine _FixedCoroutine;
        private float offsetArea;

        private void Start()
        {
            Character.animator.GetComponent<AnimationEventsManager>().AddAnimationEvent("VBoost", sfx);
            Character.animator.GetComponent<AnimationEventsManager>().AddAnimationEvent("HBoost", sfx);
            offsetArea = 1f - animationCurve.GetArea(0.001f);
            StartCoroutine(_boostValue.Regenerate());
        }

        private IEnumerator FixedCoroutine(Vector3 direction, float time, bool isHorizontal)
        {
            float currentTime = time;
            float maxTime = isHorizontal ? _horizontalBoostData.time : _verticalBoostData.time;

            if(isHorizontal)
            {
                Character.animator.SetInteger("MoveType", (int)MoveType.HorizontalBoost);
                Character.animator.SetFloat("MoveX", direction.normalized.x);
                Character.animator.SetFloat("MoveY", direction.normalized.z);
            }
            else
            {
                Character.animator.SetInteger("MoveType", (int)MoveType.VerticalBoost);
                Character.animator.SetFloat("MoveX", 0f);
                Character.animator.SetFloat("MoveY", direction.normalized.y);
            }

            while (currentTime > 0)
            {
                RotateObject();

                var force = Character.rotation * direction *
                    (offsetArea * animationCurve.Evaluate(currentTime / maxTime));
                
                Character.controller.AddRbForce(force);

                if(isHorizontal)
                {           
                    if(Character.targetHandler.isActive && isActive)
                    {
                        if(Character.targetHandler.GetCurrentTarget().distance < stopMinDistance 
                            && direction.z != 0f)
                        {
                            Character.controller.SetVelocity(Vector3.zero);
                            End();
                        }
                    }
                    Character.controller.AddRbForce(CentripetalForce(), ForceMode.Acceleration);
                }
                currentTime -= Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }          
            End();
        }

        Vector3 CentripetalForce()
        {
            TargetingSystem.ITargetData currentTarget = Character.targetHandler.GetCurrentTarget();
            float cForce = Mathf.Pow(Character.controller.velocity.magnitude, 2f) / currentTarget.distance;
            return currentTarget.direction * (-cForce * 1.195f);
        }

        private void RotateObject()
        {
            TargetingSystem.ITargetData currentTarget = Character.targetHandler.GetCurrentTarget();
            var look = Quaternion.LookRotation(-currentTarget.direction);
            var quat = Quaternion.RotateTowards(Character.rotation, look, 50f);
            quat.x = 0f;
            quat.z = 0f;

            Character.controller.Rotate(Quaternion.RotateTowards(Character.rotation, quat.normalized, 50f));
        }
     
        public void HorizontalBoost(Vector2 direction)
        {
            if(_boostValue.CanUse() && !isActive)
            {
                var dir = new Vector3(direction.x, 0f, direction.y);

                Use();
                _FixedCoroutine = StartCoroutine(FixedCoroutine(dir
                    * horizontalBoostData.distance / horizontalBoostData.time,
                    horizontalBoostData.time, true));
            }
        }
    
        public void VerticalBoost(float direction)
        {
            if (_boostValue.CanUse() && !isActive)
            {
                Use();
                StartCoroutine(FixedCoroutine(new Vector3(0f, direction, 0f)
                    * verticalBoostData.distance / verticalBoostData.time,
                    verticalBoostData.time, false));              
            }
        }

        private IEnumerator ResumeRegen()
        {
            yield return new WaitForSeconds(regenDelaySeconds);
            _boostValue.SetIsRegening(true);
        }

        private void ResetAnimatorValues()
        {
            Character.animator.SetInteger("MoveType", (int)MoveType.None); 
            Character.animator.SetFloat("MoveX", 0f);
            Character.animator.SetFloat("MoveY", 0f);
        }

        public override void Use()
        {
            base.Use();
            _boostValue.Consume();
            _boostValue.SetIsRegening(false);
            Character.movement.SetSpeedClamp(false);
    
            for(int i = 0; i < vfxHandler.Length; i++)
            {
                vfxHandler[i].Activate();
            }

            Character.movement.End();
            Character.input.DisableInput("Movement");
        }

        public override void End()
        {
            base.End();
            if(_FixedCoroutine != null)
            {
                StopCoroutine(_FixedCoroutine);
            }

            for (int i = 0; i < vfxHandler.Length; i++)
            {
                vfxHandler[i].Deactivate();
            }

            Character.animator.SetInteger("MoveType", (int)MoveType.None); 
            StartCoroutine(ResumeRegen());
            Character.movement.Use();
            Character.movement.Move(Vector2.zero);
            Character.movement.SetSpeedClamp(true);
            Character.input.EnableInput("Movement");

            Character.movement.SetDistanceToTarget
                (
                    Vector3.Distance(Character.targetHandler.GetCurrentTarget().activeCharacter.position,
                    Character.position)
                );
        }

        public override void ForceEnd()
        {
            base.ForceEnd();
            if(_FixedCoroutine != null)
            {
                StopCoroutine(_FixedCoroutine);
            }
         
            Character.controller.SetVelocity(Vector3.zero);
            ResetAnimatorValues();
        }

        public override void UltimaEnd()
        {
            base.UltimaEnd();
            for (int i = 0; i < vfxHandler.Length; i++)
            {
                vfxHandler[i].Deactivate();
            }
        }
    }
}