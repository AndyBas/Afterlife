using AfterlifeTmp.Game;
using AfterlifeTmp.Interfaces;
using AfterlifeTmp.ScriptableObjects;
using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityRandom = UnityEngine.Random;

namespace Com.MorpheusLegacy.Afterlife
{
    public class Player : MonoBehaviour, IMoveable
    {

        [SerializeField] private PlayerParametersSO _params;
        [SerializeField] private Camera _mainCamera;
        private InputActionsPlayer _inputActionsPlayer;

        private Vector3 _inputMousePos;
        private Vector3 _worldTargetPos;

        bool _isDead;
        private void Awake()
        {
            _inputActionsPlayer = new InputActionsPlayer();
            _inputActionsPlayer.Enable();

            _inputActionsPlayer.Player.Move.performed += Move_performed;
            TouchSimulation.Enable();
        }

        private void Update()
        {
            if(_isDead) return;

            Vector3 lMoveTowardsTarget = _worldTargetPos - transform.position;
            lMoveTowardsTarget *= _params.Speed * Time.deltaTime;
            lMoveTowardsTarget.z = 0;
            
            Move(lMoveTowardsTarget);
            RotateUpTowards(lMoveTowardsTarget);
        }

        private void RotateUpTowards(Vector3 pTowardsTarget)
        {
            transform.up = Vector3.Lerp(transform.up, pTowardsTarget.normalized, _params.RotaSpeed * Time.deltaTime);
        }

        public void Move(Vector3 pMoveVec)
        {
            transform.localPosition += pMoveVec;
        }

        public void OblivionChange(float pOblivionRatio)
        {
            // Play effects 
            // Material changes
        }

        public void MemoryChange(float pMemoryRatio)
        {
            // Play effects 
            // Material changes
            // Play get animations
        }

        public void Die()
        {
            _inputActionsPlayer.Player.Move.performed -= Move_performed;
            _isDead = true;
            Debug.Log("Die");
            // Play anim & effects
        }

        private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext pValue)
        {
            _inputMousePos = pValue.ReadValue<Vector2>();
            _worldTargetPos = _mainCamera.ScreenToWorldPoint(new Vector3(_inputMousePos.x, _inputMousePos.y, transform.position.z - _mainCamera.transform.position.z));
            Vector2 lClampedXYPlane = new Vector2(_worldTargetPos.x, _worldTargetPos.y);
            lClampedXYPlane = Vector2.ClampMagnitude(lClampedXYPlane, _params.MaxRadius);
            _worldTargetPos = new Vector3(lClampedXYPlane.x, lClampedXYPlane.y, _worldTargetPos.z);
        }
    }
}