using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.CameraSystem
{
    public class CameraManager : MonoBehaviour
    {
        public List<IVirtualCam> VirtualCameras;

        public void AddVirtualCamera(IVirtualCam vcam)
        {
            VirtualCameras.Add(vcam);
        }

        public void SetCameraPriority(int priority, int index)
        {
            VirtualCameras[index].SetPriority(priority);
        }


    }
}
