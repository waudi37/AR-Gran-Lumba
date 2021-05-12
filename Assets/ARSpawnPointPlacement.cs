using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARSpawnPointPlacement : MonoBehaviour
{
    [SerializeField] private GameObject spawnPointPrefab;
    [SerializeField] private CarListController carListController;
    private ARSessionOrigin arSessionOrigin;
    private ARPlaneManager arPlaneManager;
    private ARRaycastManager arRaycastManager; 
    private GameObject CollectGems; 

    private Pose placementPose;
    private bool placementPoseValid;
    private bool spawnPointPlaced;
    private Transform spawnPointTransform;

    void Awake()
    {
        arSessionOrigin = GetComponent<ARSessionOrigin>();
        arRaycastManager = GetComponent<ARRaycastManager>();
        spawnPointTransform = Instantiate(spawnPointPrefab).transform;

    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdateSpawnPoint();
        if (placementPoseValid)
        {
            if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0))
            {
                PlaceSpawnPoint();
            }
        }
    }

    private void UpdatePlacementPose()
    {
        var hits = new List<ARRaycastHit>();
        arRaycastManager.Raycast(new Vector2(Screen.width/2, Screen.height/2), hits, TrackableType.Planes);
        placementPoseValid = hits.Count > 0;
        if (placementPoseValid)
        {
            placementPose = hits[0].pose;
        }
    }

    private void UpdateSpawnPoint()
    {
        if (placementPoseValid && !spawnPointPlaced)
        {
            spawnPointTransform.gameObject.SetActive(true);
            spawnPointTransform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            spawnPointTransform.gameObject.SetActive(false);
        }
    }

    private void PlaceSpawnPoint()
    {
        if (spawnPointPlaced)
            return;
        spawnPointPlaced = true;
        var finalTransform = Instantiate(spawnPointPrefab, placementPose.position, placementPose.rotation).transform;
        finalTransform.Rotate(Vector3.up, 135);
        carListController.SetSpawnPoint(finalTransform);
        Destroy(finalTransform.gameObject);
    }
}
