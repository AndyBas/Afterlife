using Com.MorpheusLegacy.Afterlife;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp.Game
{
	public class MemoryCollectable : Collectable
	{

        [SerializeField] private float _timeDisappear = 0.5f;


        #region COLLECTABLE
        protected override void Collect()
        {
            base.Collect();

            InvokeOnCollect(1);
        }

        #endregion COLLECTABLE
    }
}