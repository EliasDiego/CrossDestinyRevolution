using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using CDR.MechSystem;

namespace CDR.InputSystem
{
    public class AITestManager : MonoBehaviour
    {
        [SerializeField]
        GameObject _TestObject;

        private void Start() 
        {
            AIMechInput AIInput = InputUtilities.AssignAIInput<AIMechInput>(_TestObject);

            AIInput.EnableInput();

            if(_TestObject.TryGetComponent<ActiveCharacter>(out ActiveCharacter activeCharacter))
                activeCharacter?.targetHandler?.Use();

            if(_TestObject.TryGetComponent<Mech>(out Mech mech))
                mech.movement?.Use();
        }
    }
}