using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVelocity : MonoBehaviour
{
	float speed;
	[SerializeField] Vector3 velocity;
	Rigidbody _rigidbody;

	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		_rigidbody.velocity += velocity * speed;
	}
}
