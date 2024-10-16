using AfterlifeTmp.Game;
using AfterlifeTmp.Interfaces;
using AfterlifeTmp.ScriptableObjects;
using Com.AndyBastel.ExperimentLab.Common.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace Com.MorpheusLegacy.Afterlife
{
    public class Player : StateObject, IMoveable
    {

        private const string _MEMORIES_RATIO_SG = "_MemoriesRatio";
        private const string _OBLIVION_RATIO_SG = "_OblivionRatio";

        [SerializeField] private PlayerParametersSO _params;
        [SerializeField] private Camera _mainCamera;

        [SerializeField] private List<GameObject> _bodies = new List<GameObject>();

        private Vector3 _inputMousePos;
        private Vector3 _worldTargetPos;

        private GameObject _body;
        private Material _material;
        bool _isTouchingScreen;

        private void Awake()
        {
            _body = _bodies[0];
            _material = _body.GetComponent<MeshRenderer>().material;

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
            _inputMousePos = pValue.ReadValue<Vector2>();
            _worldTargetPos = _mainCamera.ScreenToWorldPoint(new Vector3(_inputMousePos.x, _inputMousePos.y, transform.position.z - _mainCamera.transform.position.z));
            Vector2 lClampedXYPlane = new Vector2(_worldTargetPos.x, _worldTargetPos.y);
            lClampedXYPlane = Vector2.ClampMagnitude(lClampedXYPlane, _params.MaxRadius);
            _worldTargetPos = new Vector3(lClampedXYPlane.x, lClampedXYPlane.y, _worldTargetPos.z);
        }

        public void OnPress(UnityEngine.InputSystem.InputAction.CallbackContext pValue)
        {

        }
    }
}