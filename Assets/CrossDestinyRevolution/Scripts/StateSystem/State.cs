using System.Collections;
using System.Collections.Generic;
using CDR.MechSystem;
using UnityEngine;

namespace CDR.StateSystem
{
    public class State : MonoBehaviour, IState
    {
        IMech _sender;
        IMech _receiver;

        public IMech sender
        {
            get => _sender;
            set => _sender = value;
        }
        
        public IMech receiver 
        {  
            get => _receiver;
            set => _receiver = value;
        }

        public virtual void EndState()
        {
            _receiver.input.EnableInput();
            _receiver.currentState = null;
        }

        public virtual void StartState()
        {
            _receiver.input.DisableInput();
            DisableActions();
        }

        void DisableActions()
        {
            if(_receiver.movement.isActive) { _receiver.movement.End(); }
            if(_receiver.boost.isActive) { _receiver.boost.End(); }
            if(_receiver.meleeAttack.isActive) { _receiver.meleeAttack.End(); }
            if(_receiver.rangeAttack.isActive) { _receiver.rangeAttack.End(); }
            if(_receiver.shield.isActive) { _receiver.shield.End(); }
            if(_receiver.specialAttack1.isActive) { _receiver.specialAttack1.End(); }
            if(_receiver.specialAttack2.isActive) { _receiver.specialAttack2.End(); }
            if(_receiver.specialAttack3.isActive) { _receiver.specialAttack3.End(); }
        }
    }
}
