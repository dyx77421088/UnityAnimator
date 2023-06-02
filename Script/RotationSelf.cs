using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSelf : MonoBehaviour
{
    public float speed = 90 ;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime, Space.World);
    }
}
