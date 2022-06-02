using System.Collections;
using UnityEngine;

// This class is for the Boost system and its methods.

namespace CDR.MovementSystem
{
    public class Boost : ActionSystem.Action, IBoost
    {
        [Tooltip("Delay before resuming boost regen in seconds.")]
        [SerializeField]
        private float regenDelaySeconds;
        [SerializeField]
        private BoostValue _boostValue;
        [SerializeField]
        private VerticalBoostData _verticalBoostData;
        [SerializeField]
        private HorizontalBoostData _horizontalBoostData;

        public IBoostValue boostValue => _boostValue;
        public IBoostData horizontalBoostData => _horizontalBoostData;
        public IBoostData verticalBoostData => _verticalBoostData;

        private Controller controller;
        private int tweenID;

        private void Start()
        {
            controller = (Controller)Character.controller;
            _boostValue.ModifyValueWithoutEvent(_boostValue.MaxValue);
            StartCoroutine(_boostValue.Regenerate());
        }

        public void HorizontalBoost(Vector2 direction)
        {
            if(_boostValue.CanUse())
            {
                var dir = new Vector3(direction.x, 0f, direction.y);
                
                Character.movement.SetSpeedClamp(false);               
                _boostValue.Consume();
                _boostValue.SetIsRegening(false);

                tweenID = LeanTween.value(_horizontalBoostData.distance / _horizontalBoostData.time, 0f , _horizontalBoostData.time)
                    .setOnUpdate((float f) =>
                    {                        
                        controller.AddRbForce((transform.rotation * dir) * f);
                    }).setEaseOutExpo()
                    .setOnComplete(() =>
                    {
                        BoostEnd();                       
                    }).id;
            }
        }
    
        public void VerticalBoost(float direction)
        {
            if (_boostValue.CanUse())
            {
               
                Character.movement.SetSpeedClamp(false);
                _boostValue.Consume();
                _boostValue.SetIsRegening(false);
                var dir = direction * _verticalBoostData.distance;

                tweenID = LeanTween.value(0f, dir / _verticalBoostData.time, _verticalBoostData.time)
                    .setOnUpdate((float f) =>
                    {
                        controller.AddRbForce(new Vector3(0f, f, 0f));
                    }).setEaseInQuint()
                    .setOnComplete(() =>
                    {
                        BoostEnd();                       
                    }).id;
            }
        }

        private IEnumerator ResumeRegen()
        {
            yield return new WaitForSeconds(regenDelaySeconds);
            _boostValue.SetIsRegening(true);
        }

        private void BoostEnd()
        {
            StartCoroutine(ResumeRegen());
            Character.movement.SetSpeedClamp(true);
            Character.movement.SetDistanceToTarget
                (
                    Vector3.Distance(Character.targetHandler.GetCurrentTarget().activeCharacter.position,
                    transform.position)
                );
        }
    }
}