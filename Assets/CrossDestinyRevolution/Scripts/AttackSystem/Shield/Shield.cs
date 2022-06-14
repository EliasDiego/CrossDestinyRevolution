using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CDR.ActionSystem;
using CDR.StateSystem;

namespace CDR.AttackSystem
{
    public class Shield : Action, IShield
    {
        [SerializeField] float _radius;
        [SerializeField] Stun _stun;


        public float radius => _radius;
        public IStun stun => _stun;

        public override void Use()
        {
            base.Use();
        }

        public override void End()
        {
            base.End();
        }
    }
}
