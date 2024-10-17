using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp.Game
{
	[RequireComponent(typeof(Collider))]
	public class IkDetectionSphere : MonoBehaviour
	{
        private const float _MIN_DIST_TARGET = 0.02f;
		[SerializeField] private Transform _target;
        [SerializeField] private float _smoothSpeed = 1f;


        private Vector3 _localPos = Vector3.forward * 5;

        private void OnTriggerEnter(Collider other)
        {
            Transform lOther = other.transform;

            _localPos = transform.InverseTransformPoint(lOther.position);
        }

        private void LateUpdate()
        {
            if (Vector3.Distance(_localPos, _target.localPosition) < _MIN_DIST_TARGET)
                return;

            _target.localPosition = Vector3.MoveTowards(_target.localPosition, _localPos, _smoothSpeed * Time.deltaTime);
            _target.forward = Quaternion.AngleAxis(-90f, Vector3.right) * _localPos.normalized;
        }
    }
}