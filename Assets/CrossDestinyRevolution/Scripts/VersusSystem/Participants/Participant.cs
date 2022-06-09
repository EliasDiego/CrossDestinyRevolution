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

        public virtual void Reset()
        {
            mech.health.ModifyValue(_Mech.health.MaxValue);
            mech.boost?.boostValue.ModifyValue(_Mech.boost.boostValue.MaxValue);

            mech.input?.DisableInput();
            mech.movement?.End();
            mech.boost?.End();
            mech.targetHandler?.End();
            mech.meleeAttack?.End();
            mech.rangeAttack?.End();
            mech.specialAttack1?.End();
            mech.specialAttack2?.End();
            mech.specialAttack3?.End();

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