using AfterlifeTmp.Game;
using AfterlifeTmp.Interfaces;
using AfterlifeTmp.ScriptableObjects;
using Com.AndyBastel.ExperimentLab.Common.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityRandom = UnityEngine.Random;

namespace Com.MorpheusLegacy.Afterlife
{
    public class Player : StateObject, IMoveable
    {

        private const string _MEMORIES_RATIO_SG = "_MemoriesRatio";
        private const string _OBLIVION_RATIO_SG = "_OblivionRatio";
        private const float _BODY_ROTA_MAX_ANGLE = 45f;

        public event Action<bool, Vector2> OnPressed;

        [SerializeField] private PlayerParametersSO _params;
        [SerializeField] private Camera _mainCamera;

        [SerializeField] private List<GameObject> _bodies = new List<GameObject>();

        private Vector3 _inputMousePos;
        private Vector3 _worldTargetPos;
        private Vector3 _initialMousePos;

        private GameObject _body;
        private Material _material;
        private PlayerInput _playerInput;

        bool _isTouchingScreen;
        bool _useJoystick;
        private float _joystickRadius;

        private void Awake()
        {
            _body = _bodies[0];
            _material = _body.GetComponent<Renderer>().material;
            _playerInput = GetComponent<PlayerInput>();

            SetOblivionRatioShader(0);
            SetMemoriesRatioShader(0);
        }



        private void SetMemoriesRatioShader(float pVal)
        {
            _material.SetFloat(_MEMORIES_RATIO_SG, pVal);
        }

        private void SetOblivionRatioShader(float pVal)
        {
            _material.SetFloat(_OBLIVION_RATIO_SG, pVal);
        }

        protected override void DoActionNormal()
        {
            base.DoActionNormal();

            Vector3 lMoveTowardsTarget = _worldTargetPos - transform.position;
            lMoveTowardsTarget *= _params.Speed * Time.deltaTime;
            lMoveTowardsTarget.z = 0;

            Move(lMoveTowardsTarget);
            RotateUpTowards(lMoveTowardsTarget);
        }


        private void RotateUpTowards(Vector3 pTowardsTarget)
        {
            Vector3 lDir = pTowardsTarget.normalized;
            float lAngle = Vector3.SignedAngle(Vector3.up, lDir, Vector3.forward);

            if ((pTowardsTarget.magnitude / Time.deltaTime) < 1f)
            {
                lDir = Vector3.up;
            }
            else
            {

                if (Mathf.Abs(lAngle) > _BODY_ROTA_MAX_ANGLE)
                {
                    if (lAngle < 0)
                    {
                        lDir = Quaternion.AngleAxis(-_BODY_ROTA_MAX_ANGLE, Vector3.forward) * Vector3.up;
                    }
                    else
                        lDir = Quaternion.AngleAxis(_BODY_ROTA_MAX_ANGLE, Vector3.forward) * Vector3.up;
                }

            }

            transform.up = Vector3.Lerp(transform.up, lDir, _params.RotaSpeed * Time.deltaTime);
        }

        public void Move(Vector3 pMoveVec)
        {
            transform.localPosition += pMoveVec;
        }

        public void OblivionChange(float pOblivionRatio)
        {
            // Play effects 
            // Material changes
            SetOblivionRatioShader(pOblivionRatio);
        }

        public void MemoryChange(float pMemoryRatio)
        {
            // Play effects 
            // Material changes
            SetMemoriesRatioShader(pMemoryRatio);
            // Play get animations
        }

        public void Die()
        {
            Passive();
            Debug.Log("Die");
            // Play anim & effects
        }

        public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext pValue)
        {
            if (!_isTouchingScreen)
                return;

            if (_useJoystick)
            {
                MoveAsJoystick(pValue.ReadValue<Vector2>());
            }
            else
            {
                MoveAsFollower(pValue.ReadValue<Vector2>());
            }

        }

        private void MoveAsJoystick(Vector2 pValue)
        {
            _inputMousePos = pValue;
            Vector2 lVec = _inputMousePos - _initialMousePos;
            lVec = lVec.normalized * Mathf.Clamp01(lVec.magnitude / _joystickRadius);
            Vector2 lClampedXYPlane = new Vector2(lVec.x, lVec.y) * _params.MaxRadius;

            _worldTargetPos = new Vector3(lClampedXYPlane.x, lClampedXYPlane.y, _worldTargetPos.z);
        }

        private void MoveAsFollower(Vector2 pValue)
        {
            _inputMousePos = pValue;
            // Get input position in world
            _worldTargetPos = _mainCamera.ScreenToWorldPoint(new Vector3(_inputMousePos.x, _inputMousePos.y, transform.position.z - _mainCamera.transform.position.z));

            // Clamp inside radius
            Vector2 lClampedXYPlane = new Vector2(_worldTargetPos.x, _worldTargetPos.y);
            lClampedXYPlane = Vector2.ClampMagnitude(lClampedXYPlane, _params.MaxRadius);

            // Assign on plane XY
            _worldTargetPos = new Vector3(lClampedXYPlane.x, lClampedXYPlane.y, _worldTargetPos.z);
        }

        public void OnPress(UnityEngine.InputSystem.InputAction.CallbackContext pValue)
        {
            _isTouchingScreen = pValue.ReadValue<float>() > 0.5f;

            Vector2 lTouchPos = _playerInput.actions["Move"].ReadValue<Vector2>();

            if (_isTouchingScreen)
            {
                _initialMousePos = lTouchPos;
            }

            OnPressed?.Invoke(_isTouchingScreen, lTouchPos);
        }

        #region AB TESTS
        public void ShouldUseJoystick(bool pUseJoystick)
        {
            _useJoystick = pUseJoystick;
        }

        public void SetScreenRadius(float pRadius)
        {
            _joystickRadius = pRadius;
        }
        #endregion AB TESTS
    }
}