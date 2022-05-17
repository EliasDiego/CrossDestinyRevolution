using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.MechSystem
{
    public class ActiveCharacter : Character
    {
        public static ActiveCharacter[] activeCharacters => characterList.ToArray();
        private static List<ActiveCharacter> characterList = new List<ActiveCharacter>();

        protected override void Awake()
        {
            base.Awake();
            characterList.Add(this);
        }

        protected virtual void OnDestroy() {
            characterList.Remove(this);
        }

        // Don't include targetingHandler array
    }
}

