using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestDOT : MonoBehaviour
{
    public Gradient gradient;
    public Transform[] paths;
    void Start()
    {
        //transform.DOMove(new Vector3(-5, -5, 0), 3).OnComplete(() => Debug.Log("成功了!!!"));
        //transform.DOMove(new Vector3(5, 5, 0), 1).OnComplete(() => Debug.Log("我是第二个成功的!!!"));

        //GetComponent<MeshRenderer>().material.DOGradientColor(gradient, 3);

        transform.DOPath(paths.Select((a) => a.position).ToArray(), 4, PathType.CatmullRom, PathMode.Full3D).SetLookAt(0)
            .SetOptions(true, AxisConstraint.None, AxisConstraint.X);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
