using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;
using UnityEngine.Pool;

namespace AfterlifeTmp.Pooling
{
	public class Pool<T> where T : MonoBehaviour
	{

		private Queue<T> _currentElements = new Queue<T>();
		private T _prefab;
		private Transform _parent = null;
		public Pool(int pNb, T pPrefab, Transform pParent = null)
		{
			_prefab = pPrefab;
			_parent = pParent;

			InitializePool(pNb);
		}

        private void InitializePool(int pSize)
        {
			for (int i = 0; i < pSize; ++i)
			{
                CreateNewObject();
			}
        }

		private T CreateNewObject()
		{
			T lObj = UnityEngine.Object.Instantiate(_prefab, _parent);
			lObj.gameObject.SetActive(false);

			_currentElements.Enqueue(lObj);

			return lObj;
        }

        // Get an object from the pool
        public T Get()
        {
            if (_currentElements.Count == 0)
            {
                CreateNewObject();
            }

            T pObj = _currentElements.Dequeue();
            pObj.gameObject.SetActive(true);

            // Unparent from pool
            pObj.transform.parent = null;

            return pObj;
        }

        // Return an object back to the pool
        public void Return(T pObj)
        {
            pObj.transform.parent = _parent;

            pObj.gameObject.SetActive(false);

            _currentElements.Enqueue(pObj);
        }
    }
}