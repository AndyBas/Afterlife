using AfterlifeTmp.ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp.Managers
{
	public class GameManager : MonoBehaviour
	{

        public static GameManager Instance { get; private set; }

        [Header("Development")]
        [SerializeField] private LevelSO _devLvl;

        [Header("AB Tests")]
        public bool useJoystick = true;

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
            LevelManager.Instance.InitLevel(_devLvl);
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
        #endregion UNITY
    }
}