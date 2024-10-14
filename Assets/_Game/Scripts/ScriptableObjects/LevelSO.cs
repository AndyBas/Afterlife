using AfterlifeTmp.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp.ScriptableObjects
{
	[CreateAssetMenu(menuName = "Configs/LevelSO", fileName = "LevelSO")]
	public class LevelSO : ScriptableObject
	{
		[SerializeField] private float _conveyorSpeed = 3.0f;
		[SerializeField] private float _length = 100.0f;
		[SerializeField] private List<Pattern> _patterns = new List<Pattern>();

        public float ConveyorSpeed => _conveyorSpeed;
		public float Length => _length;
		public List<Pattern> PatternPrefabList => _patterns;
    }
}