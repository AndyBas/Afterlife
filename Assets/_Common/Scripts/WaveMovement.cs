using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    public float frequency = 1f;
    public float amplitude = 2f;
    public Vector3 waveDir = Vector3.up;
    [Range(0f, 1f)] public float initialDelay = 0f;
    public bool isLocal = false;


    private Vector3 _initPos;
    private float _elapsedT = 0f;
    // Start is called before the first frame update
    virtual protected void Start()
    {
        _initPos= isLocal ? transform.localPosition : transform.position;
        _elapsedT = initialDelay * frequency;

        Move();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        _elapsedT += Time.deltaTime * frequency;
    }

    private void Move()
    {
        if (isLocal)
        {
            transform.localPosition = _initPos + waveDir * Mathf.Sin(_elapsedT) * amplitude;
        }
        else
            transform.position = _initPos + waveDir * Mathf.Sin(_elapsedT) * amplitude;
    }
}
