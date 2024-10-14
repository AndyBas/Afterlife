using AfterlifeTmp.Game;
using AfterlifeTmp.ScriptableObjects;
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

		public static LevelManager Instance { get; private set; }

		[Header("Essentials")]
		[SerializeField] private PlayerConveyor _playerConveyor;

		[Header("Setup")]
		[SerializeField] private Pattern _endPatternPrefab;
		[SerializeField] private float _startOffset = 10f;

		[Header("Development")]
		[SerializeField] private LevelSO _devLvl;

		private List<Pattern> _patternList = new List<Pattern>();

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
        }


        private void OnDestroy()
		{
			if (Instance == this)
			{
				Instance = null;
			}
		}
		#endregion UNITY
        private void InitLevel(LevelSO pLvl)
        {
			List<Pattern> lPatterns = pLvl.PatternPrefabList;

			lPatterns.Add(_endPatternPrefab);
			int lCount = lPatterns.Count;

			Pattern lPattern = null;
			float lMemoryRatio = lCount / NB_MEMORY;
			int lNbMemory = 0;
			bool lShouldDisplayMemory = false;

            // Patterns instantiation
            // Je répartie équitablements les NB_MEMORY mémoires
			for (int i = 0; i < lCount; i++)
			{
				lPattern = Instantiate(lPatterns[i]);
				lPattern.transform.position = Vector3.forward * ((float)i/(lCount-1) * _devLvl.Length + _startOffset);

				// If there is enough pattern to potentially skip a memory: that's a security, will normally never go into the else
				if ((NB_MEMORY - lNbMemory) < (lCount - i + 1))
					lShouldDisplayMemory = i % lMemoryRatio == 0;

				else lShouldDisplayMemory = true;

				lPattern.DisplayMemory(lShouldDisplayMemory);
				if(lShouldDisplayMemory)
					lNbMemory++;

				_patternList.Add(lPattern);
            }

			_playerConveyor.InitSpeed(pLvl.ConveyorSpeed);

			// Temp
			_playerConveyor.ShouldMove(true);
        }
    }
}