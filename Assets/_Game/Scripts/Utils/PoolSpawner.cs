using AfterlifeTmp.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp.Utils
{
	public class PoolSpawner : MonoBehaviour
	{
		public EPoolType poolType;
		private Transform _curObj;

        #region UNITY
        private void OnEnable()
        {
			Spawn();
        }
		#endregion UNITY

		#region SPAWNING
		public void Spawn()
		{
			if(_curObj != null)
				return;

			_curObj = PoolManager.Instance.SpawnPoolObject(poolType);
			_curObj.parent = transform;
			_curObj.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

		}

		public void Despawn()
		{
			if (_curObj == null)
				return;

			PoolManager.Instance.ReturnPoolObject(poolType, _curObj);
			_curObj= null;
		}

		#endregion SPAWNING
	}
}