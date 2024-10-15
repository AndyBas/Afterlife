
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp.Game
{
    [RequireComponent(typeof(Camera))]
	public class GameCamera : MonoBehaviour
	{
        [SerializeField] private float _zoomCoeff = 0.5f;
        [SerializeField] private float _highSpeedFov = 120f;
        [SerializeField] private float _timeZoomInOut = 0.25f;
        [SerializeField] private AnimationCurve _zoomInOutCurve = default;

        private Camera _cam;


        private float _initialFov;
        private Vector3 _initialLocPos;
        private void Awake()
        {
            _cam = GetComponent<Camera>();

            _initialLocPos = transform.localPosition;
            _initialFov = _cam.fieldOfView;
        }

        public void ZoomInOut()
        {
            transform.DOLocalMove(_initialLocPos * _zoomCoeff, _timeZoomInOut).SetEase(_zoomInOutCurve);
        }

        public void HighSpeedFov()
        {
            _cam.fieldOfView = _highSpeedFov;
        }

        public void ResetFov()
        {
            _cam.fieldOfView = _initialFov;
        }
    }
}