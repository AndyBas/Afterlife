using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp.Game
{
	public class OblivionSphere : Collectable
	{

        #region COLLECTABLE
        protected override void Collect()
        {
            base.Collect();

            InvokeOnCollect(-1);
        }
        #endregion COLLECTABLE
    }
}