using Lean.Gui;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp.Managers
{
	public class UiManager : MonoBehaviour
	{
		public static UiManager Instance {  get; private set; }

        [SerializeField] private LeanJoystick _joystick;


        #region UNITY
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else Destroy(gameObject);   

        }

        private void Start()
        {
            _joystick.GetComponent<CanvasGroup>().alpha = 0;
        }
        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
        #endregion UNITY

        public void DisplayJoystickAt(Vector2 pScreenPos)
        {
            if(!GameManager.Instance.useJoystick)
                return;

            _joystick.GetComponent<CanvasGroup>().alpha = 1;
            _joystick.transform.position = pScreenPos;
        }

        public void HideJoystick()
        {
            if(!GameManager.Instance.useJoystick)
                return;

            _joystick.GetComponent<CanvasGroup>().alpha = 0;
        }

        public float GetJoystickRadius()
        {
            return _joystick.Radius;
        }
    }
}