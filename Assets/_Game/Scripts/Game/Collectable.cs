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
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            Player lPlayer = other.GetComponentInParent<Player>();

            if (lPlayer != null)
            {
                Collect();
            }
        }

        virtual protected void Collect()
        {
            _collider.enabled = true;

            CollectFeedbacks();
        }
        virtual protected void CollectFeedbacks()
        {
            // Play particles

            Destroy(gameObject);
        }
    }
}