using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.CameraSystem
{
    public interface IVirtualCam
    {
        public void SetPriority(int priority);
        public void SetLookTarget(Transform target);
        public void SetFollowTarget(Transform followTarget);
    }
}
