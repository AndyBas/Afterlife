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


        override protected void Awake()
        {
            base.Awake();

            _trigger.OnChildTriggerEnter += ChildTrigger_OnChildTriggerEnter;

            _light.SetActive(true);
        }

        private void ChildTrigger_OnChildTriggerEnter(Collider pObj)
        {
            _trigger.OnChildTriggerEnter -= ChildTrigger_OnChildTriggerEnter;

            Player lPlayer = pObj.GetComponentInParent<Player>();

            if (lPlayer)
            {
                OnEndReached?.Invoke();
            }
        }
    }
}