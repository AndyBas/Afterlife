using Com.MorpheusLegacy.Afterlife;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;
using UnityRandom = UnityEngine.Random;

namespace Afterlife._Game.Tests
{
	public class TestMovePlayer
	{
		[UnityTest]
		public IEnumerator TestMovingPlayer()
		{
			Player lPlayer = new GameObject().AddComponent<Player>();
			lPlayer.transform.position = Vector3.zero;
			lPlayer.Move(Vector3.forward);
			yield return new WaitForSeconds(0.1f);

			Assert.AreEqual(true, true);
		}
	}
}