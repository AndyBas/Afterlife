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


        #region COLLECTABLE
        protected override void Collect()
        {
            base.Collect();

            InvokeOnCollect(1);
        }

        protected override void CollectFeedbacks()
        {
            FeelTools.ScaleDisappear(transform.GetChild(0), _scaleTimeDisappear, callBack: () => { gameObject.SetActive(false); }, shouldDeactivate:false);
        }
        #endregion COLLECTABLE
    }
}