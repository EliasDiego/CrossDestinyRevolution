using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CDR.CameraSystem
{
    // NOTE: Check if world rotation would affect x or z axis through front and backward movement
    // Adjust distance checking accordingly.
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
            var difference = Mathf.Floor(diffY);
            var interpolation = Mathf.Abs(diffY) / 50f;

            // Character is below target
            if (difference < -1f)
            {     
                currentY = Mathf.Lerp(defaultY, 0f, interpolation);
                currentZ = defaultZ;
            }
            // Character is above target
            else if (difference > 1f)
            {               
                currentY = Mathf.Lerp(defaultY, maxY, interpolation);               
                currentZ = Mathf.Lerp(defaultZ, maxZ, interpolation) + 0.35f;
            }
            else
            {
                currentY = defaultY;
                currentZ = defaultZ;
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

            return Mathf.Abs(activeCharacter.targetHandler.GetCurrentTarget().activeCharacter.position.x -
                transform.position.x) <= minDistance;
        }
    }
}
