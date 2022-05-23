using System.Collections;
using UnityEngine;
using CDR.MechSystem;
using System;

// This class is for the Boost system and its methods.

namespace CDR.MovementSystem
{
    public class Boost : ActionSystem.Action, IBoost
    {
        [SerializeField]
        private BoostValue _boostValue;
        [SerializeField]
        private VerticalBoostData _verticalBoostData;
        [SerializeField]
        private HorizontalBoostData _horizontalBoostData;

        public IBoostValue boostValue => _boostValue;

        public IBoostData horizontalBoostData => _horizontalBoostData;

        public IBoostData verticalBoostData => _verticalBoostData;

        public bool isActive => base.isActive;

        public IActiveCharacter Character => base.Character;

        public event System.Action onUse;
        public event System.Action onEnd;

        public void End()
        {
            base.End();
        }

        public void HorizontalBoost(Vector2 direction)
        {
            if(_boostValue.CanUse() && isActive)
            {
                onUse?.Invoke();
                _boostValue.Consume();

                var dir = transform.position + 
                    new Vector3(direction.x, 0f, direction.y) * _horizontalBoostData.distance;

                LeanTween.move(gameObject, dir, _horizontalBoostData.time).setEaseOutExpo()
                    .setOnComplete(() =>
                    {
                        onEnd?.Invoke();
                    }); 
            }
        }

        public void Use()
        {
            base.Use();
        }

        public void VerticalBoost(float direction)
        {
            onUse?.Invoke();
            _boostValue.Consume();

            if(_boostValue.CanUse() && isActive)
            {
                var dir = transform.position.y + direction * _verticalBoostData.distance;

                LeanTween.moveY(gameObject, dir, _horizontalBoostData.time).setEaseOutExpo()
                    .setOnComplete(() =>
                    {
                        onEnd?.Invoke();
                    });
            }
        }

        private void Start()
        {
            _boostValue.ModifyValueWithoutEvent(_boostValue.MaxValue);
            StartCoroutine(_boostValue.Regenerate());
        }
    }
}