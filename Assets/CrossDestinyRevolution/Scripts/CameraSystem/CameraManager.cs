using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.CameraSystem
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] playerCams;
        [SerializeField]
        private Cinemachine.CinemachineTargetGroup targetGroup;

        public List<IVirtualCam> VirtualCameras;

        public void AddVirtualCamera(IVirtualCam vcam)
        {
            VirtualCameras.Add(vcam);
        }

        public void SetCameraPriority(int priority, int index)
        {
            VirtualCameras[index].SetPriority(priority);
        }

        public void SetPlayerCam(Transform player, int index)
        {
            IVirtualCam vcam = playerCams[index].GetComponent<IVirtualCam>();
            if (vcam != null)
            {
                vcam.SetFollowTarget(player.GetComponentInChildren<CameraPivot>().transform);
                vcam.SetLookTarget(targetGroup.transform);
                targetGroup.AddMember(player, 1f, 0f);
            }
        }
    }
}
