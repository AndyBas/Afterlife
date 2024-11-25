using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace AfterlifeTmp
{
	public class UpWaveMovement : WaveMovement
	{
        protected override void Start()
        {
            base.Start();

            waveDir = transform.up;
        }
    }
}