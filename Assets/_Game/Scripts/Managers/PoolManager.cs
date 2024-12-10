using AfterlifeTmp.Game;
using AfterlifeTmp.Pooling;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp.Managers
{
	public enum EPoolType
	{
		Oblivion = 0,
		Memory
	}

	[Serializable]
	public class PoolData<T> where T : MonoBehaviour
	{
		public int initialSize = 10;
		public T prefab;
		public Transform parent;
	}

	public class PoolManager : MonoBehaviour
	{
		public static PoolManager Instance { get; private set; }

		[SerializeField] private PoolData<OblivionSphere> _poolDataOblivion;
		[SerializeField] private PoolData<MemoryCollectable> _poolDataMemory;

		private Pool<OblivionSphere> _poolOblivion;
		private Pool<MemoryCollectable> _poolMemory;

        private void Awake()
        {
            // Singleton pattern to ensure only one instance exists
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

			_poolOblivion = new Pool<OblivionSphere>(_poolDataOblivion.initialSize, _poolDataOblivion.prefab, _poolDataOblivion.parent);
			_poolMemory = new Pool<MemoryCollectable>(_poolDataMemory.initialSize, _poolDataMemory.prefab, _poolDataMemory.parent);
        }

		public Transform SpawnPoolObject(EPoolType pPoolType)
		{
			switch (pPoolType)
			{
				case EPoolType.Oblivion:
					return _poolOblivion.Get().transform;
				case EPoolType.Memory:
					return _poolMemory.Get().transform;
				default:
					return null;
			}
		}

		public void ReturnPoolObject(EPoolType pPoolType, Transform pObj)
		{
			switch (pPoolType)
			{
				case EPoolType.Oblivion:
					_poolOblivion.Return(pObj.GetComponent<OblivionSphere>());
					break;
				case EPoolType.Memory:
					_poolMemory.Return(pObj.GetComponent<MemoryCollectable>());
					break;
				default:
					break;
			}
		}
    }
}