using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp.Utils
{
	[Serializable]
	public class LocalSavedData
	{
		public bool isVolumeEnabled = true;
		public bool isLanguageEnglish = true;
		public bool hasPlayedOnce = false;
		public int nbLevelsCompleted = 0;
		public int[] levelsCompletion = new int[100];
	}
}