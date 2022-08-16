using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.MechSystem;

namespace CDR.CameraSystem.NewStuff
{
    public class CameraTester : MonoBehaviour
    {
        [SerializeField]
        CameraHandler _CameraHandler;
        [SerializeField]
        ActiveCharacter _ActiveCharacter;
        
        void Start()
        {
            _CameraHandler = Instantiate(_CameraHandler.gameObject, _ActiveCharacter.transform)?.GetComponent<CameraHandler>();

            _CameraHandler.activeCharacter = _ActiveCharacter;

            _CameraHandler.Enable();
        }
    }
}