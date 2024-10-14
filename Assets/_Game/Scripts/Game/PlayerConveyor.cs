using AfterlifeTmp.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp.Game
{
	public class PlayerConveyor : MonoBehaviour, IMoveable
	{
		[Header("Hit'n'Stop")]
		[SerializeField] private float _timeHitNStop = 0.5f;
		[SerializeField] private float _hitNStopSpeed = 0.1f;

		[Header("Acceleration")]
		[SerializeField] private float _accelerationCoeff = 2.5f;

		private bool _isMoving = false;
		private float _curForwardSpeed = 0f;
		private float _initForwardSpeed = 0f;

        Coroutine _changeSpeedRoutine;

        private void Update()
        {
            if(_isMoving)
            {
                Move(transform.forward * _curForwardSpeed * Time.deltaTime);
            }
        }

        public void InitSpeed(float pSpeed)
        {
            _initForwardSpeed = _curForwardSpeed = pSpeed;
        }

        public void ShouldMove(bool pShouldMove)
		{
			_isMoving = pShouldMove;
		}

        public void Move(Vector3 pMoveVec)
        {
            transform.position += pMoveVec;
        }

        public void PlayHitNStop()
        {
            if (_changeSpeedRoutine != null)
                return;

            _changeSpeedRoutine = StartCoroutine(ChangeSpeedRoutine(_hitNStopSpeed, _timeHitNStop));
        }

        private IEnumerator ChangeSpeedRoutine(float pTempSpeed, float pTime)
        {
            float lUsualSpeed = _curForwardSpeed;
            _curForwardSpeed = pTempSpeed;
            yield return new WaitForSeconds(pTime);

            _curForwardSpeed = lUsualSpeed;

            _changeSpeedRoutine = null;
        }
    }
}