using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem.New
{
    public struct HitExitData : IHitExitData
    {
        private IHitShape _HitShape;
        private IHurtShape _HurtShape;

        public IHitShape hitShape => _HitShape;

        public IHurtShape hurtShape => _HurtShape;

        public HitExitData(IHitShape hitShape, IHurtShape hurtShape)
        {
            _HitShape = hitShape;
            _HurtShape = hurtShape;
        }
    }
}