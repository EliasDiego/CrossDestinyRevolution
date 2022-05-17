using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;

namespace CDR.InputSystem
{
    public abstract class CharacterInput<T> : MonoBehaviour where T : ActiveCharacter
    {
        private T _Character;

        protected T character => _Character;

        protected virtual void Awake()
        {
            _Character = GetComponent<T>();
        }
    }
}