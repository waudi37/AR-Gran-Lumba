using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTargetManager : MonoBehaviour
{
    ARTrackedImageManager imageManager;
    void Awake()
    {
        imageManager = GameObject.FindObjectOfType<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        imageManager.trackedImagesChanged += Changed;
    }

    void OnDisable()
    {
        imageManager.trackedImagesChanged -= Changed;
    }

    // Update is called once per frame
    void Changed(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.updated)
        {
            
        }
    }
}
