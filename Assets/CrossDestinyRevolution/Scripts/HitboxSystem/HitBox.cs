using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.MechSystem;

namespace CDR.HitboxSystem
{
	public class HitBox : MonoBehaviour, IHitDetector
	{
		[SerializeField] BoxCollider m_BoxCollider;
		[SerializeField] LayerMask m_layerMask;

		[SerializeField] Vector3 m_hitBoxSize = Vector3.one;

		public IHitResponder m_hitResponder;
		public IHitResponder HitResponder { get => m_hitResponder; set => m_hitResponder = value; }

		public void CheckHit()
		{
			float _distance = m_hitBoxSize.y;
			Vector3 _direction = transform.up;
			Vector3 _center = transform.TransformPoint(m_BoxCollider.center);
			Vector3 _start = transform.position;
			Vector3 _halfExtends = new Vector3(m_hitBoxSize.x, m_hitBoxSize.y, m_hitBoxSize.z) / 2;
			Quaternion _orientation = transform.rotation;

			RaycastHit[] _hits = Physics.BoxCastAll(_start, _halfExtends, _direction, _orientation, _distance, m_layerMask);


			foreach (RaycastHit _hit in _hits)
			{
				IHurtBox _hurtbox = _hit.collider.GetComponentInChildren<IHurtBox>();
				_hurtbox.hurtResponder.Response(m_hitResponder.Damage);
				HitResponder.Response();
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
			Gizmos.DrawCube(Vector3.zero, new Vector3(m_hitBoxSize.x, m_hitBoxSize.y, m_hitBoxSize.z));
		}
	}
}

