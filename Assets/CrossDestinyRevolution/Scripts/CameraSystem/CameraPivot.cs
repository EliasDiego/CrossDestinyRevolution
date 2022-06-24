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
        }

        private void SetPos()
        {
            var diffY = activeCharacter.position.y -
                activeCharacter.targetHandler.GetCurrentTarget().activeCharacter.position.y;

            currentY = Mathf.Clamp01(Mathf.Abs(diffY / maxY));
            currentZ = Mathf.Clamp01(1f - Mathf.Abs(currentY));
            
            var y = Mathf.Lerp(defaultY, maxY, currentY);
            var z = Mathf.Lerp(Mathf.Abs(maxZ), Mathf.Abs(defaultZ), currentZ) * -1f;

            if(diffY > 0f)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, y, z);
            }
            if(diffY < 0f)
            {
                var tempY = Mathf.Lerp(defaultY, 0f, currentY);
                transform.localPosition = new Vector3(transform.localPosition.x, tempY, transform.localPosition.z);

            }
        } 
    }
}
