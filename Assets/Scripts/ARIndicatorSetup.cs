using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System;

public class ARIndicatorSetup : MonoBehaviour
{

    public GameObject placementIndicator;
    public GameObject groundPlane;
    private ARRaycastManager raycastManager;
    private Pose placementPose;

    private Camera currentCamera;

    private bool placementPoseIsValid = false;

    private bool isPlaneSetup = false;

    private WaveManager2 waveManager;

    private Text setupText;

    void Start()
    {
        setupText = GameObject.Find("SetupText").GetComponent<Text>();
        raycastManager = FindObjectOfType<ARRaycastManager>();  
        currentCamera = FindObjectOfType<Camera>();
        waveManager = FindObjectOfType<WaveManager2>();
        waveManager.enabled = false;
        groundPlane.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaneSetup) return;
        UpdatePlacementPose();
        UpdatePlacementIndicator();
        if (TryGetTouchPosition(out Vector2 touchPosition) && placementPoseIsValid)
        {
            setupText.text = "";
            setupText.enabled = false;
            isPlaneSetup = true;
            groundPlane.SetActive(true);
            groundPlane.transform.SetPositionAndRotation(placementPose.position,placementPose.rotation);
            placementIndicator.SetActive(false);
            waveManager.BeginWaves(placementPose.position);
            waveManager.enabled = true;

            //Save point
            //Deploy place
            //Deploy enemies
        }
    }


    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;
            touchPosition = new Vector2(mousePosition.x, mousePosition.y);
            return true;
        }
#else
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
#endif

        touchPosition = default;
        return false;
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            setupText.text = "Click anywhere to begin";
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position,placementPose.rotation);
        }
        else
        {
            setupText.text = "Scan the floor until an indicator appears";
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {   
        var screenCenter = currentCamera.ViewportToScreenPoint(new Vector3(0.5f,0.5f));
        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);
        
        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = currentCamera.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
}
