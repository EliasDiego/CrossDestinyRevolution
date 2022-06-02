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
		[SerializeField] SphereCollider m_SphereCollider;
		[SerializeField] LayerMask m_layerMask;
		[SerializeField] bool isSphere;

		[SerializeField] Vector3 m_hitBoxSize = Vector3.one;
		[SerializeField] float m_radius = 0.025f;

		private IHitResponder m_hitResponder;

		public IHitResponder HitResponder { get => m_hitResponder; set => m_hitResponder = value; }

		public void CheckHit()
		{
			if(!isSphere)
			{
				Vector3 _scaledSize = new Vector3(
					m_BoxCollider.size.x * m_hitBoxSize.x,
					m_BoxCollider.size.y * m_hitBoxSize.y,
					m_BoxCollider.size.z * m_hitBoxSize.z);

				float _distance = _scaledSize.y - m_hitBoxSize.y;
				Vector3 _direction = transform.up;
				Vector3 _center = transform.TransformPoint(m_BoxCollider.center);
				Vector3 _start = transform.position;
				Vector3 _halfExtends = new Vector3(_scaledSize.x, _scaledSize.y, _scaledSize.z) / 2;
				Quaternion _orientation = transform.rotation;

				RaycastHit[] _hits = Physics.BoxCastAll(_start, _halfExtends, _direction, _orientation, _distance, m_layerMask);

				CheckCollisions(_hits, _center);
			}
			
			if(isSphere)
			{
				Vector3 _direction = transform.up;
				Vector3 _center = transform.TransformPoint(m_SphereCollider.center);
				Vector3 _start = transform.position;

				RaycastHit[] _hits = Physics.SphereCastAll(_start, m_radius, _direction, m_layerMask);

				CheckCollisions(_hits, _center);
			}
		}

		void CheckCollisions(RaycastHit[] _hits, Vector3 _center)
		{
			HitData _hitdata;
			IHurtBox _hurtbox;
			foreach (RaycastHit _hit in _hits)
			{
				_hurtbox = _hit.collider.GetComponent<IHurtBox>();
				if (_hurtbox != null)
				{
					if (_hurtbox.Active)
					{
						_hitdata = new HitData
						{
							damage = m_hitResponder == null ? 0 : m_hitResponder.Damage,
							hitPoint = _hit.point == Vector3.zero ? _center : _hit.point,
							hitNormal = _hit.normal,
							hurtbox = _hurtbox,
							hitDetector = this
						};

						if (_hitdata.Validate())
						{
							_hitdata.hitDetector.HitResponder?.Response(_hitdata);
							_hitdata.hurtbox.hurtResponder?.Response(_hitdata);
						}
					}
				}
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
			
			if (!isSphere)
				Gizmos.DrawCube(Vector3.zero, new Vector3(m_hitBoxSize.x, m_hitBoxSize.y, m_hitBoxSize.z));
			if (isSphere)
				Gizmos.DrawSphere(Vector3.zero, m_radius); // Because size is halfExtents
		}
	}
}

