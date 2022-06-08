using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

using CDR.MechSystem;
using System;

namespace CDR.InputSystem
{
    public abstract class PlayerCharacterInput<T> : PlayerInput 
        where T : IActiveCharacter
    {
        private T _Character;

        protected T character => _Character;

        protected virtual void Awake()
        {
            _Character = GetComponent<T>();

            if(_Character != null)
                _Character.input = this;
        }
    }
}