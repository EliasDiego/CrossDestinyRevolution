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
    public interface IBox
    {
        Vector3 size { get; }
    }

    public interface IHurtBox : IBox
    {
        IActiveCharacter activeCharacter  { get; }
        event Action<IActiveCharacter> onHurt;
    }

    public interface IHitbox : IBox
    {
        float damage { get; }
        event Action<IActiveCharacter> onHit;
    }

    public interface IBullet : IProjectile
    {
        IHitbox hitbox { get; }
        float speed { get; }
        Vector3 direction { get; }
    }

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
        IStun stun { get; }
    }

    public interface ISpecialAttack : ICooldownAction
    {

    }
}