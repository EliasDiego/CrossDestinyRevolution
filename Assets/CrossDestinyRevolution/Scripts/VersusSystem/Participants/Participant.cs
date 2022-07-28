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
            
            Transform transform = (mech as Mech).transform; 
            
            transform.position = _StartPosition;
            transform.rotation = _StartRotation;
        }

        public virtual void Start()
        {
            mech.input?.EnableInput();
            mech.targetHandler?.Use();
            mech.movement?.Use();
        }

        public virtual void Stop()
        {
            mech.input?.DisableInput();

            mech.targetHandler?.End();

            if(CheckBoolean(mech.shield?.isActive))
                mech.shield?.Stop();

            mech.specialAttack1?.Stop();
            mech.specialAttack2?.Stop();
            mech.specialAttack3?.Stop();

            if(CheckBoolean(mech.meleeAttack?.isActive))
                mech.meleeAttack?.Stop();

            mech.rangeAttack?.Stop();

            if(CheckBoolean(mech.boost?.isActive))
                mech.boost?.Stop();
            
            mech.movement?.Stop();
    
            mech.animator.SetInteger("StateType", (int)AnimationSystem.StateType.None);
            mech.animator.SetInteger("ActionType", (int)AnimationSystem.ActionType.None);
            mech.animator.SetInteger("MoveType", (int)AnimationSystem.MoveType.Movement);

            mech.animator.SetFloat("StateSMultiplier", 1);
            mech.animator.SetFloat("ActionSMultiplier", 1);
            mech.animator.SetFloat("MoveSMultiplier", 1);

            mech.controller?.SetVelocity(Vector3.zero);
        }
    }
}