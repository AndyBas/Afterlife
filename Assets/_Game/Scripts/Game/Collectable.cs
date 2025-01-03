using Com.MorpheusLegacy.Afterlife;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp.Game
{

    [RequireComponent(typeof(Collider))]
    public class Collectable : MonoBehaviour
    {
        public static event Action<int> OnCollect;
        private Collider _collider;

        [SerializeField] protected float _scaleTimeDisappear = 0.5f;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<IkDetectionSphere>() != null)
                return;

            Player lPlayer = other.GetComponentInParent<Player>();

            if (lPlayer != null)
            {
                Collect();
            }
        }

        virtual protected void Collect()
        {
            _collider.enabled = false;
            transform.parent = null;

            CollectFeedbacks();
        }
        virtual protected void CollectFeedbacks()
        {
            // Play particles
            FeelTools.ScaleDisappear(transform, _scaleTimeDisappear,callBack: ResetObject );
        }

        protected void InvokeOnCollect(int pValue)
        {
            OnCollect?.Invoke(pValue);
        }

        virtual protected void ResetObject()
        {
            _collider.enabled = true;
        }
    }
}