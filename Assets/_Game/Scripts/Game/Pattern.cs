using AfterlifeTmp.Managers;
using AfterlifeTmp.Utils;
using Com.AndyBastel.ExperimentLab.Common.Collisions;
using Com.MorpheusLegacy.Afterlife;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp.Game
{
	public class Pattern : MonoBehaviour
	{
		[SerializeField] private GameObject _memory = null;

		protected ChildTrigger3D _trigger = null;
		protected PoolSpawner[] _poolSpawners;

		[Header("Procedural Purposes")]
		[SerializeField, Range(0, 5)] private int _difficulty = 1;

        public int Difficulty => _difficulty;

        virtual protected void Awake()
        {
			_poolSpawners = GetComponentsInChildren<PoolSpawner>();

            _trigger = GetComponentInChildren<ChildTrigger3D>();

            _trigger.OnChildTriggerExit += Trigger_OnChildTriggerExit;
        }

        virtual protected void Trigger_OnChildTriggerExit(Collider pObj)
        {
			Player lPlayer = pObj.GetComponentInParent<Player>();

			if (lPlayer != null)
			{
				FeelTools.ScaleDisappear(transform, callBack: DespawnCollectables);
			}
        }

        private void DespawnCollectables()
        {
			foreach (PoolSpawner lPool in _poolSpawners)
			{
				lPool.Despawn();
			}
        }

        public void DisplayMemory(bool pShouldDisplay)
		{
			_memory.SetActive(pShouldDisplay);
		}
	}
}