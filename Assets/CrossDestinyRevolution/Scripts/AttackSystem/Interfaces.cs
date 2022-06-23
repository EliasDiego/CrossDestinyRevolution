using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;
using CDR.StateSystem;
using CDR.ActionSystem;
using CDR.MovementSystem;
using CDR.ObjectPoolingSystem;

namespace CDR.AttackSystem
{
    public interface IRangeAttack : ICooldownAction
    {
        float range { get; }
    }

    public interface IMeleeAttack : ICooldownAction
    {
        IHitShape hitbox { get; }
        float speed { get; }
    }

    public interface IShield : IAction
    {
        float radius { get; }
        //IStun stun { get; }
        IHurtShape hurtSphere{ get; }
    }

    public interface ISpecialAttack : ICooldownAction
    {

    }
}