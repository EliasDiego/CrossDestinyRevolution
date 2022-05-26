using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;

namespace CDR.InputSystem
{
    public abstract class CharacterInput<T> : MonoBehaviour, IInput where T : IActiveCharacter
    {
        private T _Character;

        protected T character => _Character;

        protected virtual void Awake()
        {
            _Character = GetComponent<T>();

            if(_Character != null)
                _Character.input = this;
        }

        public abstract void EnableInput();
        public abstract void DisableInput();
    }
}