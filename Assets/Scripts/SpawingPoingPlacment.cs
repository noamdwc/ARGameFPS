using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class SpawingPoingPlacment : MonoBehaviour
{
    public GameObject myObject1;
    private ARSessionOrigin arOrigin;
    private Pose plasementPose;
    private bool valid = false;
    private bool first = true;

    private ARRaycastManager raycastManager;


    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        myObject1.SetActive(false);


    }
    // Update is called once per frame
    void Update()
    {
        updatePlacement();
        updatePlacementObject();
    }

    private void updatePlacementObject()
    {
        if (first)
        {
            if (valid)
            {
                first = false;
                myObject1.transform.SetPositionAndRotation(plasementPose.position, plasementPose.rotation);
                myObject1.SetActive(true);
            }
        }
    }

    private void updatePlacement()
    {
        var screanCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();

        raycastManager.Raycast(screanCenter, hits, TrackableType.Planes);
        valid = hits.Count > 0;
        if (valid)
        {
            plasementPose = hits[0].pose;

        }
    }
}
