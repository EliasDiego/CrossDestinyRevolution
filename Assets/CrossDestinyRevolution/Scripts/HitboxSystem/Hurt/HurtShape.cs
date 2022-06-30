using System;
using System.Collections;
using System.Collections.Generic;
using CDR.MechSystem;
using UnityEngine;

namespace CDR.AttackSystem
{
    public abstract class HurtShape : CollisionShape, IHurtShape
    {
        public override event Action<IHitData> onHitEnter;
        public override event Action<IHitData> onHitExit;

        public void HitEnter(IHitData hitData)
        {
            onHitEnter?.Invoke(hitData);
        }

        public void HitExit(IHitData hitData)
        {
            onHitExit?.Invoke(hitData);
        }

        protected override void OnDrawGizmos() 
        {
            base.OnDrawGizmos();

            Gizmos.color = Color.yellow;
        }
    }
}