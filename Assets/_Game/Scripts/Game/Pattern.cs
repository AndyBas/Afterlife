using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp.Game
{
	public class Pattern : MonoBehaviour
	{
		[SerializeField] private MemoryCollectable _memory = null;

		[Header("Procedural Purposes")]
		[SerializeField, Range(1, 5)] private int _difficulty;

        public int Difficulty => _difficulty;

        public void DisplayMemory(bool pShouldDisplay)
		{
			_memory.gameObject.SetActive(pShouldDisplay);
		}
	}
}