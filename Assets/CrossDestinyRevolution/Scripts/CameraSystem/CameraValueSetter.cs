using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.CameraSystem
{
    public class CameraValueSetter : MonoBehaviour
    {
        [SerializeField]
        private CameraManager manager;
        [SerializeField]
        private Transform[] players;


        private void Start()
        {
            manager.SetPlayerCam(players[0], 0);
            manager.SetPlayerCam(players[1], 1);
        }
    }
}
