using System;
using System.Collections;
using System.Collections.Generic;
using CDR.MechSystem;
using UnityEngine;

namespace CDR.AttackSystem
{
    public abstract class HurtShape : CollisionShape, IHurtShape
    {
        public override event Action<IHitEnterData> onHitEnter;
        public override event Action<IHitExitData> onHitExit;

        public void HitEnter(IHitEnterData hitData)
        {
            onHitEnter?.Invoke(hitData);
        }

        public void HitExit(IHitExitData hitData)
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