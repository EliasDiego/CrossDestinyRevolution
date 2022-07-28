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
        
        public virtual void StartState()
        {
            if(_receiver != null)
            {
                _receiver.input.DisableInput();

                DisableActions();
            }
        }

        public virtual void EndState()
        {
            if(_receiver != null)
            {
                _receiver.input.EnableInput();

                _receiver.currentState = null;
                EnableActions();
            }
        }

        public virtual void ForceEndState()
        {
            if(_receiver != null)
            {
                _receiver.input.EnableInput();

                _receiver.currentState = null;
                EnableActions();
            }
        }

        void DisableActions()
        {
            if(_receiver.movement.isActive) { _receiver.movement.ForceEnd(); }
            if(_receiver.boost.isActive) { _receiver.boost.ForceEnd(); }
            if(_receiver.meleeAttack.isActive) { _receiver.meleeAttack.ForceEnd(); }
            if(_receiver.rangeAttack.isActive) { _receiver.rangeAttack.ForceEnd(); }
            if(_receiver.shield.isActive) { _receiver.shield.ForceEnd(); }
            if(_receiver.specialAttack1.isActive) { _receiver.specialAttack1.ForceEnd(); }
            if(_receiver.specialAttack2.isActive) { _receiver.specialAttack2.ForceEnd(); }
            if(_receiver.specialAttack3.isActive) { _receiver.specialAttack3.ForceEnd(); }
        }

        void EnableActions()
        {
            //Need to enable movement after state is finished

            if(!_receiver.movement.isActive) { _receiver.movement.Use(); }
            //if(!_receiver.boost.isActive) { _receiver.boost.Use(); }
            //if(!_receiver.meleeAttack.isActive) { _receiver.meleeAttack.Use(); }
            //if(!_receiver.rangeAttack.isActive) { _receiver.rangeAttack.Use(); }
            //if(!_receiver.shield.isActive) { _receiver.shield.Use(); }
            //if(!_receiver.specialAttack1.isActive) { _receiver.specialAttack1.Use(); }
            //if(!_receiver.specialAttack2.isActive) { _receiver.specialAttack2.Use(); }
            //if(!_receiver.specialAttack3.isActive) { _receiver.specialAttack3.Use(); }
        }
    }
}
