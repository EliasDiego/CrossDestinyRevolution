using System.Collections;
using UnityEngine;

// This class handles gravity simulation on a rigidbody.

namespace CDR.MovementSystem
{
    public class Gravity : MonoBehaviour
    {
        [SerializeField]
        private float pullOnDistance;
        [SerializeField]
        private Rigidbody rb;
        [SerializeField]
        private float pullSpeed = 12f;
        [SerializeField]
        private bool enableGravity = true;

        private void Start()
        {
            StartCoroutine(GravityOnRigidbody());
        }


        // Enable gravity on object relative to flight plane.
        private IEnumerator GravityOnRigidbody()
        {
            while (true)
            {
                if (enableGravity && GetDistance() > pullOnDistance)
                {
                    rb.AddForce(-rb.transform.up * pullSpeed, ForceMode.Acceleration);
                }
                yield return null;
            }
        }

        private float GetDistance()
        {
            return Vector3.Distance(rb.transform.position, transform.position);
        }
    }
}
