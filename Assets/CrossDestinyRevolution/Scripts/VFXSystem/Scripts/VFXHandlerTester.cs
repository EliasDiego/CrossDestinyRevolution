using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CDR.VFXSystem;

public class VFXHandlerTester : MonoBehaviour
{
    [SerializeField]
    private bool _IsActive = false;

    IVFXHandler _VFXHandler;
    
    void Start()
    {
        _VFXHandler = GetComponent<IVFXHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_IsActive && !_VFXHandler.isActive)
            _VFXHandler.Activate();

        else if(!_IsActive && _VFXHandler.isActive)
            _VFXHandler.Deactivate();
    }
}
