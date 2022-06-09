using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public struct HitEnterData :  IHitEnterData
    {
        private IHitShape _HitShape;
        private IHurtShape _HurtShape;
        private Vector3 _CollisionPoint;
        private Vector3 _CollisionNormal;

        public IHitShape hitShape => _HitShape;

        public IHurtShape hurtShape => _HurtShape;

        public Vector3 collisionPoint => _CollisionPoint;

        public Vector3 collisionNormal => _CollisionNormal;

        public HitEnterData(IHitShape hitShape, IHurtShape hurtShape, RaycastHit raycastHit)
        {
            _HitShape = hitShape;
            _HurtShape = hurtShape;
            _CollisionPoint = raycastHit.point;
            _CollisionNormal = raycastHit.normal;
        }
    }
}