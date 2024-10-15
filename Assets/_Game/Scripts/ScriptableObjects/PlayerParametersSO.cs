using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp.ScriptableObjects
{
	[CreateAssetMenu(menuName = "Configs/PlayerParametersSO", fileName = "PlayerParametersSO")]
	public class PlayerParametersSO : ScriptableObject
	{
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _rotaSpeed = 5f;
        [SerializeField] private float _maxRadius = 3;

        public float Speed => _speed;
        public float RotaSpeed => _rotaSpeed;
        public float MaxRadius => _maxRadius;    
    }
}