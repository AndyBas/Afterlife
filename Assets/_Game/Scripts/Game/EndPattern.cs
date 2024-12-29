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
	public class EndPattern : Pattern
    {
        public event Action OnEndReached;
        [SerializeField] private GameObject _light;

        public Vector3 EndPoint => _light.transform.position;

        override protected void Awake()
        {
            base.Awake();

            _light.SetActive(true);
        }

        protected override void Trigger_OnChildTriggerExit(Collider pObj)
        {
            _trigger.OnChildTriggerExit -= Trigger_OnChildTriggerExit;

            Player lPlayer = pObj.GetComponentInParent<Player>();

            if (lPlayer)
            {
                OnEndReached?.Invoke();
            }
        }
    }
}