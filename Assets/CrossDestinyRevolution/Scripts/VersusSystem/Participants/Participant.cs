using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;
using CDR.UISystem;

namespace CDR.VersusSystem
{
    public class Participant : IParticipant
    {
        IMech _Mech;
        Vector3 _StartPosition;
        Quaternion _StartRotation;

        public int score { get; set; }
        public IMech mech => _Mech;

        public Participant(IMech mech, Vector3 startPosition, Quaternion startRotation)
        {
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
            mech.specialAttack1?.End();

            if(CheckBoolean(mech.specialAttack2?.isActive))
            mech.specialAttack2?.End();

            if(CheckBoolean(mech.specialAttack3?.isActive))
            mech.specialAttack3?.End();

            if(CheckBoolean(mech.meleeAttack?.isActive))
            mech.meleeAttack?.End();

            if(CheckBoolean(mech.rangeAttack?.isActive))
            mech.rangeAttack?.End();

            if(CheckBoolean(mech.boost?.isActive))
                mech.boost?.End();
            
            if(CheckBoolean(mech.movement?.isActive))
                mech.movement?.End();

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