using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 360f;
    public bool isLocal = true;
    public Vector3 axis = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocal)
        {
            transform.localRotation = Quaternion.AngleAxis(speed * Time.deltaTime, axis) * transform.localRotation;
        }
        else transform.rotation = Quaternion.AngleAxis(speed * Time.deltaTime, axis) * transform.rotation;
    }
}
