using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class SpawingPoint : MonoBehaviour
{
    public CircularMotion creating;

    public GameObject aroundPoint;

    public float spawingTime;

    private float time = 0;


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= spawingTime)
        {
            CircularMotion obj = Instantiate(creating);
            obj.aroundPoint = aroundPoint;
            time = 0;
        }
    }
}
