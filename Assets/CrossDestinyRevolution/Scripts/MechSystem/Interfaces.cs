using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.InputSystem;
using CDR.StateSystem;
using CDR.AttackSystem;
using CDR.MovementSystem;
using CDR.TargetingSystem;

namespace CDR.MechSystem
{
    public interface IHealth : IValueRange
    {
        event System.Action OnDeath;
        void TakeDamage(float damage);
        void CheckHealthStatus();
        void Death();
    }

    public interface ICharacter
    {
        Animator animator { get; }
        AudioSource audioSource { get; }
    }

    public interface IActiveCharacter : ICharacter
    {
        Vector3 position { get; }
        IHealth health { get; }
        IHurtShape[] hurtBoxes { get; }
        ICharacterController controller { get; }
        IInput input { get; set; }
        IState currentState { get; set; }
        ITargetHandler targetHandler { get; }
        IMovement movement { get; } 
    }

    public interface IMech : IActiveCharacter
    {
        IBoost boost { get; }
        IMeleeAttack meleeAttack { get; }
        IRangeAttack rangeAttack { get; }
        IShield shield { get; }
        ISpecialAttack specialAttack1 { get; }
        ISpecialAttack specialAttack2 { get; }
        ISpecialAttack specialAttack3 { get; }
    }
}