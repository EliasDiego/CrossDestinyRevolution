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
        private float maxY, maxZ;
        [Tooltip("Minimum Z distance between target and object before allowing pivot movement.")]
        [SerializeField]
        private float minDistance = 10f;
        [SerializeField]
        private bool allowPivotMove = true;

        private float currentY = 0f;
        private float currentZ = 0f;
        private float defaultY, defaultZ;
        private bool isDefault = true;

        private void Start()
        {
            defaultY = transform.localPosition.y;
            defaultZ = transform.localPosition.z;
        }

        private void Update()
        {
            if(activeCharacter.targetHandler.isActive)
            {
                allowPivotMove = IsInDistance();
                if (allowPivotMove)
                {                
                    SetPos();
                    isDefault = false;
                }
                else
                {
                    if (!isDefault)
                    {
                        transform.localPosition = new Vector3(transform.localPosition.x, defaultY, defaultZ);
                        isDefault = true;
                    }
                }
            }
        }

        private void SetPos()
        {
            var diffY = YDiffToTarget();
            var difference = Mathf.Round(diffY);
            var interpolation = Mathf.Abs(diffY) / 20f;

            if (difference == 0f)
            {
                currentY = defaultY;
                currentZ = defaultZ;
                return;
            }
            // Character is below target
            if (difference < 0f)
            {                   
                currentY = Mathf.Lerp(defaultY, 0f, interpolation);
            }
            // Character is above target
            if (difference > 0f)
            {               
                currentY = Mathf.Lerp(defaultY, maxY, interpolation);               
                currentZ = Mathf.Lerp(defaultZ, maxZ, interpolation) + 0.35f;
            }
            transform.localPosition = new Vector3(transform.localPosition.x, currentY, currentZ);
        }

        private float YDiffToTarget()
        {
            return activeCharacter.position.y -
                activeCharacter.targetHandler.GetCurrentTarget().activeCharacter.position.y;
        }

        private bool IsInDistance()
        {           
            if(Mathf.Round(YDiffToTarget()) > 0f)
            {
                return true;
            }


            return Mathf.Abs(activeCharacter.targetHandler.GetCurrentTarget().activeCharacter.position.z -
                transform.position.z) <= minDistance;
        }
    }
}
