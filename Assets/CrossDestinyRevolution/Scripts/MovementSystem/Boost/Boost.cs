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



        public override void End()
        {
            base.End();
        }

        public void HorizontalBoost(Vector2 direction)
        {
            if(_boostValue.CanUse())
            {
                var dir = transform.position + 
                    new Vector3(direction.x, 0f, direction.y) * _horizontalBoostData.distance;
                Vector3 point = dir;

                if(Physics.Raycast(transform.position, -(transform.position - dir), out RaycastHit hit, _horizontalBoostData.distance))
                {
                    point = hit.point;
                }

                _boostValue.Consume();
                _boostValue.SetIsRegening(false);

                LeanTween.move(gameObject, point, _horizontalBoostData.time).setEaseOutQuad()
                    .setOnComplete(() =>
                    {
                        _boostValue.SetIsRegening(true);
                    }); 
            }
        }

        public override void Use()
        {
            base.Use();
        }

        public void VerticalBoost(float direction)
        {
            if(_boostValue.CanUse())
            {
                _boostValue.Consume();
                _boostValue.SetIsRegening(false);
                var dir = direction * _verticalBoostData.distance;

                LeanTween.moveY(gameObject, dir, _horizontalBoostData.time).setEaseOutExpo()
                    .setOnComplete(() =>
                    {
                        _boostValue.SetIsRegening(true);
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