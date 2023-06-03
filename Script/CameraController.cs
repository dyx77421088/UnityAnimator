using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    private Vector3 py; // Æ«ÒÆÁ¿
    void Start()
    {
        py = transform.position - target.position;
        //transform.position = target.position + py;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + target.TransformDirection(py);
        //transform.position = target.position + py;
        transform.LookAt(target);
    }
}
