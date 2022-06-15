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
        private float maxY;
        [SerializeField]
        private float maxZ;
        [SerializeField]
        private float maxYDiff = 10f;

        private float currentY = 0f;
        private float currentZ = 0f;
        private float defaultY;
        private float defaultZ;

        private void Start()
        {
            defaultY = transform.localPosition.y;
            defaultZ = transform.localPosition.z;
        }

        private void Update()
        {
            SetPos();
            ChangeYZ();
        }

        private void SetPos()
        {
            var y = Mathf.Lerp(defaultY, maxY, currentY);
            var z = Mathf.Lerp(Mathf.Abs(maxZ), Mathf.Abs(defaultZ), currentZ) * -1f;
            transform.localPosition = new Vector3(transform.localPosition.x, y, z);
        }
    
        private void ChangeYZ()
        {
            var diffY = activeCharacter.position.y -
                activeCharacter.targetHandler.GetCurrentTarget().activeCharacter.position.y;

            currentY = diffY / maxYDiff;
            currentZ = 1f - Mathf.Abs(currentY);

            //if(diffY > 0f)
            //{
            //    Debug.Log(currentZ);
            //}
        }     
    }
}
