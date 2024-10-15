using AfterlifeTmp.Game;
using AfterlifeTmp.ScriptableObjects;
using Com.MorpheusLegacy.Afterlife;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp.Managers
{
	public class LevelManager : MonoBehaviour
	{
		const int NB_MEMORY = 10;
		const int NB_OBLIVION = 3;

		public static LevelManager Instance { get; private set; }

		[Header("Essentials")]
		[SerializeField] private PlayerConveyor _playerConveyor;
		[SerializeField] private Player _player;
		[SerializeField] private GameCamera _camera;

		[Header("Setup")]
		[SerializeField] private EndPattern _endPatternPrefab;
		[SerializeField] private float _startOffset = 10f;

		[Header("Development")]
		[SerializeField] private LevelSO _devLvl;

		private List<Pattern> _patternList = new List<Pattern>();
		private EndPattern _endPattern;

		private int _curNbOblivion = 0;
		private int _curNbMemory = 0;	

		private float MemoryRatio => (float)_curNbMemory / NB_MEMORY;
		private float OblivionRatio => (float)_curNbOblivion / NB_OBLIVION;

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
            if(GameManager.Instance == null)
			{
				InitLevel(_devLvl);
			}

            Collectable.OnCollect += Collectable_OnCollect;
        }

        private void OnDestroy()
		{
			if (Instance == this)
			{
				Instance = null;
            }
            Collectable.OnCollect -= Collectable_OnCollect;
        }
		#endregion UNITY
        private void InitLevel(LevelSO pLvl)
        {
			List<Pattern> lPatterns = pLvl.PatternPrefabList;
			int lCount = lPatterns.Count;

			Pattern lPattern = null;
			float lMemoryRatio = lCount / NB_MEMORY;
			int lNbMemory = 0;
			bool lShouldDisplayMemory = false;

            // Patterns instantiation
            // Je r�partie �quitablements les NB_MEMORY m�moires
			for (int i = 0; i < lCount; i++)
			{
				lPattern = Instantiate(lPatterns[i]);
				lPattern.transform.position = Vector3.forward * ((float)i/lCount * pLvl.Length + _startOffset);

				// If there is enough pattern to potentially skip a memory: that's a security, will normally never go into the else
				if ((NB_MEMORY - lNbMemory) < (lCount - i + 1))
					lShouldDisplayMemory = i % lMemoryRatio == 0;

				else lShouldDisplayMemory = true;

				lPattern.DisplayMemory(lShouldDisplayMemory);
				if(lShouldDisplayMemory)
					lNbMemory++;

				_patternList.Add(lPattern);
            }

			lPattern = Instantiate(_endPatternPrefab);
			lPattern.transform.position = Vector3.forward * (pLvl.Length + _startOffset * 2);
			_endPattern = (EndPattern)lPattern;
            _endPattern.OnEndReached += EndPattern_OnEndReached;


			_playerConveyor.InitSpeed(pLvl.ConveyorSpeed);

			// Temp
			_playerConveyor.ShouldMove(true);
        }

        private void EndPattern_OnEndReached()
        {
            _endPattern.OnEndReached -= EndPattern_OnEndReached;
            _playerConveyor.ShouldMove(false);
            Debug.Log("Level Finished");
        }

        private void Collectable_OnCollect(int pVal)
        {
            // If it is a positive collectable
            if (pVal > 0)
            {
                CollectMemory();
            }
            // If it is a negative collectable
            else
            {
                CollectOblivion();
            }
        }

        private void CollectOblivion()
        {
			++_curNbOblivion;

            _camera.ZoomInOut();

            _playerConveyor.PlayHitNStop();
			_player.OblivionChange(OblivionRatio);

			if (_curNbOblivion == NB_OBLIVION)
			{
				PlayerDeath();
			}
        }

		private void CollectMemory()
		{
			++_curNbMemory;

			_player.MemoryChange(MemoryRatio);
		}

        private void PlayerDeath()
        {
            _endPattern.OnEndReached -= EndPattern_OnEndReached;

            _playerConveyor.ShouldMove(false);
			_player.Die();
		}
	}
}