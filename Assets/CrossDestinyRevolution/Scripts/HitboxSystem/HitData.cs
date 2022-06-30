using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.AttackSystem
{
    public struct HitData : IHitData
    {
        public IHitShape hitShape { get; }

        public IHurtShape hurtShape { get; }

        public HitData(IHitShape hitShape, IHurtShape hurtShape)
        {
            this.hitShape = hitShape;
            this.hurtShape = hurtShape; 
        }
    }
}