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
        private const float _BASE_HEAD_X_ANGLE = -220f;
        private const float _BASE_HEAD_Y_OFFSET = 20f;
		[SerializeField] private Transform _target;
        [SerializeField] private float _smoothSpeed = 1f;
        [SerializeField] private float _rotaSmoothSpeed = 180f;

        private Transform _objectTracked;
        private Vector3 _localPos = Vector3.forward * 5;

        private void OnTriggerEnter(Collider other)
        {
            if (_objectTracked)
                return;

            _objectTracked = other.transform;

            TrackObject();
        }

        private void OnTriggerStay(Collider other)
        {

            // If there is no object tracked assign a new one
            if (!_objectTracked)
                _objectTracked = other.transform;
            else if (other.transform != _objectTracked)
                return;

            TrackObject();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform != _objectTracked)
                return;

            _objectTracked = null;
            _localPos = Vector3.forward;
        }

        private void LateUpdate()
        {
            // Update the ik target position and rotation
            Quaternion lTargetRota = Quaternion.AngleAxis(_BASE_HEAD_X_ANGLE, Vector3.right) * Quaternion.LookRotation(new Vector3(-_localPos.x, _localPos.y, _localPos.z));
            _target.localPosition = Vector3.MoveTowards(_target.localPosition, _localPos + Vector3.up * _BASE_HEAD_Y_OFFSET, _smoothSpeed * Time.deltaTime);
            _target.localRotation = Quaternion.Slerp(_target.localRotation, lTargetRota, _rotaSmoothSpeed * Time.deltaTime);
        }

        private void TrackObject()
        {
            _localPos = transform.InverseTransformPoint(_objectTracked.position);
        }
    }
}