using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;
using CDR.UISystem;

namespace CDR.VersusSystem
{
    public class Participant : IParticipant
    {
        private IMech _Mech;
        private Vector3 _StartPosition;
        private Quaternion _StartRotation;
        private string _Name;

        public int score { get; set; }
        public IMech mech => _Mech;
        public string name => _Name;

        public Participant(string name, IMech mech, Vector3 startPosition, Quaternion startRotation)
        {
            _Name = name;
            _Mech = mech;
            _StartPosition = startPosition;
            _StartRotation = startRotation;

            score = 0;
        }

        private bool CheckBoolean(bool? boolean)
        {
            return boolean.HasValue && boolean.Value;
        }

        public virtual void Reset()
        {
            mech.health.ModifyValue(_Mech.health.MaxValue);
            mech.boost?.boostValue.ModifyValue(_Mech.boost.boostValue.MaxValue);

            mech.input?.DisableInput();

            if(CheckBoolean(mech.targetHandler?.isActive))
                mech.targetHandler?.End();

            if(CheckBoolean(mech.specialAttack1?.isActive))
            mech.specialAttack1?.ForceEnd();

            if(CheckBoolean(mech.specialAttack2?.isActive))
            mech.specialAttack2?.ForceEnd();

            if(CheckBoolean(mech.specialAttack3?.isActive))
            mech.specialAttack3?.ForceEnd();

            if(CheckBoolean(mech.meleeAttack?.isActive))
            mech.meleeAttack?.ForceEnd();

            if(CheckBoolean(mech.rangeAttack?.isActive))
            mech.rangeAttack?.ForceEnd();

            if(CheckBoolean(mech.boost?.isActive))
                mech.boost?.ForceEnd();
            
            if(CheckBoolean(mech.movement?.isActive))
                mech.movement?.ForceEnd();

            mech.controller?.SetVelocity(Vector3.zero);
            
            (mech as Mech).transform.position = _StartPosition;
        }

        public virtual void Start()
        {
            mech.input?.EnableInput();
            mech.targetHandler?.Use();
            mech.movement?.Use();
        }
    }
}