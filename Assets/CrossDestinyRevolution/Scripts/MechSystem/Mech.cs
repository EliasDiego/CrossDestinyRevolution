using System.Collections;
using System.Collections.Generic;
using CDR.AttackSystem;
using CDR.MovementSystem;
using UnityEngine;

namespace CDR.MechSystem
{
    public class Mech : ActiveCharacter, IMech
    {
        Boost _boost;
        IMeleeAttack _meleeAttack;
        IRangeAttack _rangeAttack;
        IShield _shield;
        ISpecialAttack _specialAttack1;
        ISpecialAttack _specialAttack2;
        ISpecialAttack _specialAttack3;

        public IBoost boost => _boost;

        public IMeleeAttack meleeAttack => _meleeAttack;

        public IRangeAttack rangeAttack => _rangeAttack;

        public IShield shield => _shield;

        public ISpecialAttack specialAttack1 => _specialAttack1;

        public ISpecialAttack specialAttack2 => _specialAttack2;

        public ISpecialAttack specialAttack3 => _specialAttack3;

        protected override void Awake()
        {
            base.Awake();

            //_boost = GetComponent<IBoost>();
            _meleeAttack = GetComponent<IMeleeAttack>();
            _rangeAttack = GetComponent<IRangeAttack>();
            _shield = GetComponent<IShield>();
            // TO BE CHANGED
            _specialAttack1 = GetComponent<ISpecialAttack>();
            _specialAttack2 = GetComponent<ISpecialAttack>();
            _specialAttack3 = GetComponent<ISpecialAttack>();
        }
    }
}

