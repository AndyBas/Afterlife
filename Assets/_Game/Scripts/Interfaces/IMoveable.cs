using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp.Interfaces
{
	public interface IMoveable
	{

        public void Move(Vector3 pMoveVec);

    }
}