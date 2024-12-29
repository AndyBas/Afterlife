using AfterlifeTmp.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp.Ui
{
	public class MenuScreenController : UiController
	{
		private const string _BUTTON_PLAY_NAME = "ButtonPlay";
		private const string _BUTTON_SETTINGS_NAME = "ButtonSettings";
		private const string _BUTTON_SHOP_NAME = "ButtonShop";

		// Level Selection
		private const string _LEVEL_SELECTION_NAME = "LevelSelection";
		private const string _BUTTON_NEXT_LVL = "ButtonNext";
		private const string _BUTTON_PREVIOUS_LVL = "ButtonPrevious";
		private const string _BUTTON_CHOOSE_LVL = "ButtonLevel";

		// Settings
		private const string _SETTINGS_NAME = "Settings";
		private const string _BUTTON_VOLUME = "ButtonVolume";
		private const string _BUTTON_LANGUAGE = "ButtonLanguage";
		private const string _BUTTON_CLOSE_SETTINGS = "ButtonCloseSettings";

        public event Action OnPlayClicked;
        public event Action OnSettingsClicked;
        public event Action<int> OnLevelChose;
        public event Action OnVolumeChanged;
        public event Action OnLocalizationChanged;

		private Button _btnPlay;
		private Button _btnSettings;
		private Button _btnShop;

		// LevelSelection
		private VisualElement _lvlSelection;
		private Button _btnNext;
		private Button _btnPrevious;
		private Button _btnChoose;

        // Settings
        [SerializeField] private Sprite _sprVolumeOn = null;
        [SerializeField] private Sprite _sprVolumeOff = null;
        [Space]
        [SerializeField] private Sprite _sprLangEng = null;
        [SerializeField] private Sprite _sprLangFr = null;

        private VisualElement _settings;
        private Button _btnVolume;
        private Button _btnLanguage;
        private Button _btnCloseSettings;

        private int _curLevel;
        private int _maxLevel;

        #region UNITY
        protected override void Awake()
        {
            base.Awake();

            HideLevelSelection();
            HideSettings();
        }
        #endregion UNITY

        #region UICONTROLLER

        protected override void AssignElements()
        {
            _btnPlay = _root.Q<Button>(_BUTTON_PLAY_NAME);
            _btnShop = _root.Q<Button>(_BUTTON_SHOP_NAME);
            _btnSettings = _root.Q<Button>(_BUTTON_SETTINGS_NAME);

            _lvlSelection = _root.Q<VisualElement>(_LEVEL_SELECTION_NAME);
            _btnPrevious = _lvlSelection.Q<Button>(_BUTTON_PREVIOUS_LVL);
            _btnChoose = _lvlSelection.Q<Button>(_BUTTON_CHOOSE_LVL);
            _btnNext = _lvlSelection.Q<Button>(_BUTTON_NEXT_LVL);

            _settings = _root.Q<VisualElement>(_SETTINGS_NAME);
            _btnVolume = _settings.Q<Button>(_BUTTON_VOLUME);
            _btnLanguage = _settings.Q<Button>(_BUTTON_LANGUAGE);
            _btnCloseSettings = _settings.Q<Button>(_BUTTON_CLOSE_SETTINGS);
        }
        protected override void SubscribeToEvents()
        {

            _btnPlay.clicked += BtnPlay_Clicked;
            _btnShop.clicked += BtnShop_Clicked;
            _btnSettings.clicked += BtnSettings_Clicked;

            _btnPrevious.clicked += BtnPrevious_Clicked;
            _btnChoose.clicked += BtnChoose_Clicked;
            _btnNext.clicked += BtnNext_Clicked;

            _btnVolume.clicked += BtnVolume_Clicked;
            _btnLanguage.clicked += BtnLanguage_Clicked;
            _btnCloseSettings.clicked += BtnCloseSettings_Clicked;
        }

        protected override void UnsubscribeToEvents()
        {
            _btnPlay.clicked -= BtnPlay_Clicked;
            _btnShop.clicked -= BtnShop_Clicked;
            _btnSettings.clicked -= BtnSettings_Clicked;

            _btnPrevious.clicked -= BtnPrevious_Clicked;
            _btnChoose.clicked -= BtnChoose_Clicked;
            _btnNext.clicked -= BtnNext_Clicked;

            _btnVolume.clicked -= BtnVolume_Clicked;
            _btnLanguage.clicked -= BtnLanguage_Clicked;
            _btnCloseSettings.clicked -= BtnCloseSettings_Clicked;
        }
        #endregion UICONTROLLER


        #region DISPLAYABILTY
        public void DisplayLevelSelection(int pMaxLevel, int[] pLevelsCompletionRate)
        {
            _lvlSelection.style.display = DisplayStyle.Flex;
        }
        private void HideLevelSelection()
        {
            _lvlSelection.style.display = DisplayStyle.None;
        }

        public void DisplaySettings(LocalSavedData pSavedData)
        {
            _settings.style.display = DisplayStyle.Flex;

            UpdateData(pSavedData);
        }

        private void HideSettings()
        {
            _settings.style.display = DisplayStyle.None;
        }

        public void UpdateData(LocalSavedData pSavedData)
        {
            StyleBackground lBg = new StyleBackground(pSavedData.isVolumeEnabled ? _sprVolumeOn : _sprVolumeOff);
            _btnVolume.style.backgroundImage = lBg;

            lBg = new StyleBackground(pSavedData.isLanguageEnglish ? _sprLangEng : _sprLangFr);
            _btnLanguage.style.backgroundImage = lBg;

        }
        #endregion DISPLAYABILTY

        #region EVENTS
        private void BtnNext_Clicked()
        {
            // Display Next Level
            // Update buttons interactability
            throw new NotImplementedException();
        }

        private void BtnChoose_Clicked()
        {
            // Launch selected level
            throw new NotImplementedException();
        }

        private void BtnPrevious_Clicked()
        {
            // Display previous level
            // Update buttons interactibility
            throw new NotImplementedException();
        }

        private void BtnSettings_Clicked()
        {
            // Display Settings pop up
            OnSettingsClicked?.Invoke();
        }

        private void BtnShop_Clicked()
        {
            // Display Shop pop up
            throw new NotImplementedException();
        }

        private void BtnPlay_Clicked()
        {
            // Display Level Selection
            OnPlayClicked?.Invoke();
        }

        private void BtnCloseSettings_Clicked()
        {
            HideSettings();
        }

        private void BtnLanguage_Clicked()
        {
            OnLocalizationChanged?.Invoke();
        }

        private void BtnVolume_Clicked()
        {
            OnVolumeChanged?.Invoke(); 
        }
        #endregion EVENTS
    }
}