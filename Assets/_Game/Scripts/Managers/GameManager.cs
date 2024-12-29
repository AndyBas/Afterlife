using AfterlifeTmp.ScriptableObjects;
using AfterlifeTmp.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp.Managers
{
	public class GameManager : MonoBehaviour
	{
        private const string _KEY_LOCAL_SAVED_DATA = "local-saved-data";
        private const int _GAME_LVL_BUILD_IND = 2;
        private const float _SCENE_TRANSITION_T = 0.5f;
        public static GameManager Instance { get; private set; }

        [SerializeField] private List<LevelSO> _levels = new List<LevelSO>();

        [Header("Development")]
        [SerializeField] private LevelSO _devLvl;

        [Header("AB Tests")]
        public bool useJoystick = true;

        private LocalSavedData _localData;
        private int _currentLevel;
        public int CurrentLevel => _currentLevel;
        public LocalSavedData LocalData => _localData;
        public int[] LevelsCompletionRate => _localData.levelsCompletion;
        #region UNITY
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void Start()
        {
            _localData = new LocalSavedData();

            LocalizationSettings.PreloadBehavior = PreloadBehavior.PreloadAllLocales;

            if (ES3.KeyExists(_KEY_LOCAL_SAVED_DATA))
            {
                _localData = ES3.Load(_KEY_LOCAL_SAVED_DATA) as LocalSavedData;
                Debug.Log(_localData);
            }

            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localData.isLanguageEnglish ? 0 : 1];

        }

        private void OnApplicationQuit()
        {
            SaveLocalData();
        }

        private void OnApplicationPause(bool pPause)
        {
            if (pPause)
                SaveLocalData();
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
        #endregion UNITY

        #region FLOW
        public void LoadGameLevel(int pLvl)
        {
            if (pLvl < 0 || pLvl > _levels.Count - 1)
            {
                Debug.LogError("Level chosen out of range");
                return;
            }

            _currentLevel = pLvl;
            StartCoroutine(LoadGameLvlRoutine());
        }

        private IEnumerator LoadGameLvlRoutine()
        {
            UiManager.Instance.FadeInWhiteScreen(_SCENE_TRANSITION_T);
            yield return new WaitForSeconds(_SCENE_TRANSITION_T);

            SceneManager.LoadScene(_GAME_LVL_BUILD_IND);

            yield return null;

            LevelManager.Instance.InitLevel(_levels[_currentLevel]);
            UiManager.Instance.FadeOutWhiteScreen(_SCENE_TRANSITION_T);
        }
        #endregion FLOW

        #region LOCALDATA
        private void SaveLocalData()
        {
            ES3.Save(_KEY_LOCAL_SAVED_DATA, _localData);
        }
        public void ChangeVolume()
        {
            // Change local data
            _localData.isVolumeEnabled = !_localData.isLanguageEnglish;
            // Apply changes to audiomanager

            SaveLocalData();

        }
        public void ChangeLanguage()
        {
            // Change local data
            _localData.isLanguageEnglish = !_localData.isLanguageEnglish;
            // Apply changes to Localization
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localData.isLanguageEnglish ? 1 : 0];
            SaveLocalData();
        }

        public void FinishLevel(int pLevelCompletedInd, int pCompletion)
        {
            // To prevent from changing the max nb completed if the player is redoing a previous level
            if(pLevelCompletedInd == _localData.nbLevelsCompleted)
                _localData.nbLevelsCompleted = pLevelCompletedInd + 1;

            _localData.levelsCompletion[_localData.nbLevelsCompleted] = pCompletion;

            if(pLevelCompletedInd == 0)
                _localData.hasPlayedOnce = true;

            SaveLocalData();
        }

        #endregion LOCALDATA
    }
}