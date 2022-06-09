using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.CameraSystem
{
    public class CameraPivot : MonoBehaviour
    {
        [SerializeField]
        private MechSystem.ActiveCharacter activeCharacter;
        [SerializeField]
        private float maxX;
        [SerializeField]
        private float maxY;

        [Range(0f,1f)]
        public float tempX = 0f;
        [Range(0f,1f)]
        public float tempY = 0f;

        private void SetPos()
        {
            var x = Mathf.Lerp(0f, maxX, tempX);
            var y = Mathf.Lerp(0f, maxY, tempY);
            transform.localPosition = new Vector3(x, y, transform.localPosition.z);
        }

        private void Update()
        {
            SetPos();
            Debugger();
        }

        private void Debugger()
        {
            var diff = activeCharacter.position.y -
                activeCharacter.targetHandler.GetCurrentTarget().activeCharacter.position.y;

            if(diff > 0f)
            {
                

                Debug.Log(diff);
            }
        }
    }
}
