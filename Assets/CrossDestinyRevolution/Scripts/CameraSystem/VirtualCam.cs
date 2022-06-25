using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace CDR.CameraSystem
{
    public class VirtualCam : MonoBehaviour, IVirtualCam
    {
        private CinemachineVirtualCamera vCamera;

        private void Awake()
        {
            vCamera = GetComponent<CinemachineVirtualCamera>();
        }

        public void SetFollowTarget(Transform followTarget)
        {
            vCamera.Follow = followTarget;
        }

        public void SetLookTarget(Transform target)
        {
            vCamera.LookAt = target;
        }

        public void SetPriority(int priority)
        {
            vCamera.Priority = priority;
        }       


    }
}

