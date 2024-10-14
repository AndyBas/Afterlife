using AfterlifeTmp.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityRandom = UnityEngine.Random;

namespace Com.MorpheusLegacy.Afterlife
{
    public class Player : MonoBehaviour, IMoveable
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _maxRadius = 3;
        [SerializeField] private Camera _mainCamera;
        private InputActionsPlayer _inputActionsPlayer;


        private Vector3 _inputMousePos;
        private Vector3 _worldTargetPos;
        private void Awake()
        {
            _inputActionsPlayer = new InputActionsPlayer();
            _inputActionsPlayer.Enable();

            _inputActionsPlayer.Player.Move.performed += Move_performed;
            TouchSimulation.Enable();
        }

        private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext pValue)
        {
            _inputMousePos = pValue.ReadValue<Vector2>();
            _worldTargetPos = _mainCamera.ScreenToWorldPoint(new Vector3(_inputMousePos.x, _inputMousePos.y, transform.position.z - _mainCamera.transform.position.z));
            Vector2 lClampedXYPlane = new Vector2(_worldTargetPos.x, _worldTargetPos.y);
            lClampedXYPlane = Vector2.ClampMagnitude(lClampedXYPlane, _maxRadius);
            _worldTargetPos = new Vector3(lClampedXYPlane.x, lClampedXYPlane.y, _worldTargetPos.z);
        }

        private void Update()
        {
            Vector3 lMoveTowardsTarget = _worldTargetPos - transform.position;
            lMoveTowardsTarget *= _speed * Time.deltaTime;
            lMoveTowardsTarget.z = 0;
            
            Move(lMoveTowardsTarget);
        }

        public void Move(Vector3 pMoveVec)
        {
            transform.localPosition += pMoveVec;
        }
    }
}