using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVelocity : MonoBehaviour
{
	[SerializeField] float speed;
	[SerializeField] Vector3 velocity;
	public Rigidbody _rigidbody;

	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		_rigidbody.velocity += velocity.normalized * speed;
	}
}
