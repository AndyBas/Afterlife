using AfterlifeTmp.Ui;
using Lean.Gui;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp.Managers
{
	public class UiManager : MonoBehaviour
	{
		public static UiManager Instance {  get; private set; }

        [SerializeField] private LeanJoystick _joystick;
        [SerializeField] private CanvasGroup _whiteScreen;
        [SerializeField] private MenuScreenController _menuScreenController;
        [SerializeField] private EndScreenController _endScreenController;

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
            _endScreenController.HideScreen();

            _menuScreenController.OnPlayClicked += MenuScreenController_OnPlayClicked;
            _menuScreenController.OnSettingsClicked += MenuScreenController_OnSettingsClicked;
            _menuScreenController.OnLevelChose += MenuScreenController_OnLevelChose;
            _menuScreenController.OnVolumeChanged += MenuScreenController_OnVolumeChanged;
            _menuScreenController.OnLocalizationChanged += MenuScreenController_OnLocalizationChanged;
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
        #endregion UNITY

        #region WHITESCREEN
        public void FadeInWhiteScreen(float pTime)
        {
            StartCoroutine(FadeWhiteScreenRoutine(pTime, true));
        }
        public void FadeOutWhiteScreen(float pTime)
        {
            StartCoroutine(FadeWhiteScreenRoutine(pTime, false));
        }

        private IEnumerator FadeWhiteScreenRoutine(float pTime, bool pFadeIn)
        {
            float lT = 0f;
            float lRatio = 0f;
            while (lT < pTime)
            {
                lRatio = lT / pTime;
                _whiteScreen.alpha = pFadeIn ?  lRatio : 1 - lRatio;
                lT += Time.deltaTime;
                yield return null;
            }

            _whiteScreen.alpha = 1;
        }
        #endregion WHITESCREEN

        #region JOYSTICK
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
        #endregion JOYSTICK

        #region MENUSCREEN
        private void MenuScreenController_OnSettingsClicked()
        {
            _menuScreenController.DisplaySettings(GameManager.Instance.LocalData);
        }
        private void MenuScreenController_OnLocalizationChanged()
        {
            GameManager.Instance.ChangeLanguage();
            _menuScreenController.UpdateData(GameManager.Instance.LocalData);
        }

        private void MenuScreenController_OnVolumeChanged()
        {
            GameManager.Instance.ChangeVolume();
            _menuScreenController.UpdateData(GameManager.Instance.LocalData);
        }

        private void MenuScreenController_OnLevelChose(int pLvl)
        {
            GameManager.Instance.LoadGameLevel(pLvl);
        }

        private void MenuScreenController_OnPlayClicked()
        {
            _menuScreenController.DisplayLevelSelection(GameManager.Instance.CurrentLevel, GameManager.Instance.LevelsCompletionRate);
        }
        #endregion MENUSCREEN
    }
}