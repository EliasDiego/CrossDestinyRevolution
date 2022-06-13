using System.Collections;
using UnityEngine;

// This class handles gravity simulation on a rigidbody.

namespace CDR.MovementSystem
{
    public class Gravity : MonoBehaviour
    {
        [Tooltip("Minimum y distance from flight plane before applying gravity")]
        [SerializeField]
        private float minDistToPull;
        [SerializeField]
        private CharacterController controller;
        [SerializeField]
        private float pullSpeed = 12f;
        [SerializeField]
        private bool enableGravity = true;

        private float defaultY;

        private void Start()
        {
            defaultY = transform.position.y;
            StartCoroutine(GravityOnRigidbody());
        }

        public void SetEnableGravity(bool isEnabled)
        {
            enableGravity = isEnabled;
        }

        // Enable gravity on object relative to flight plane.
        private IEnumerator GravityOnRigidbody()
        {
            while (true)
            {
                if (enableGravity && GetDistance() > minDistToPull)
                {
                    controller.AddRbForce(-transform.up * pullSpeed, ForceMode.Acceleration);
                }
                yield return null;
            }
        }

        private float GetDistance()
        {
            return transform.position.y - defaultY;
        }
    }
}
