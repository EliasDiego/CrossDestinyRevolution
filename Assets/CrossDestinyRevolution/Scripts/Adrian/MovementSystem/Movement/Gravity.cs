using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float pullOnDistance;
    [SerializeField]
    private float pullSpeed = 12f;
    [SerializeField]
    private bool enableGravity = true;

    private void Start()
    {
        StartCoroutine(GravityOnRigidbody());
    }

    private IEnumerator GravityOnRigidbody()
    {
        while(true)
        {
            if(enableGravity)
            {
                rb.AddForce(-transform.up * pullSpeed, ForceMode.Acceleration);
            }
            yield return null;
        }
    }
}
