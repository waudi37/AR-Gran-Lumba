using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CarListController : MonoBehaviour
{
    [System.Serializable] public class SpawnPointEvent: UnityEngine.Events.UnityEvent<Transform> {}
    public SpawnPointEvent OnSpawnPointSet, OnTappedDrive, OnTappedBack;
    [SerializeField] private List<GameObject> carList;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private CarController carController;
    [SerializeField] private TMPro.TMP_Text carLabel;

    int carIndex = 0;
    public GameObject currentCar;
    private bool isPlaneValid;
    private Vector3 hitPoint;
    private float scaleFactor = 1;
    private Vector3 originalScale;

    [SerializeField] private CanvasGroup selectionScreen, drivingScreen;

    void Start()
    {
        carIndex = PlayerPrefs.GetInt("SavedCarIndex", 0);
        SetSpawnPoint(spawnPoint);
    }

    public void SetSpawnPoint(Transform spawnPoint)
    {
        Debug.Log("SpawnpointSet", this);
        this.spawnPoint = spawnPoint;
        OpenSelectionScreen();
        SpawnCar();
        if (OnSpawnPointSet != null)
            OnSpawnPointSet.Invoke(spawnPoint);
    }

    private void SpawnCar()
    {
        if (spawnPoint)
        {
            if (currentCar)
            {
                scaleFactor = currentCar.transform.localScale.x / originalScale.x;
                Destroy(currentCar);
            }
            currentCar = Instantiate(carList[carIndex], spawnPoint.position, spawnPoint.rotation);
            originalScale = currentCar.transform.localScale;
            currentCar.transform.localScale = originalScale * scaleFactor;
            spawnPoint = currentCar.transform;
            carLabel.text = currentCar.name.Replace("(Clone)", "");
        }
    }

    public void GoToPrevious()
    {
        carIndex--;
        carIndex = carIndex < 0 ? carList.Count - 1 : carIndex;
        PlayerPrefs.SetInt("SavedCarIndex", carIndex);
        SpawnCar();
    }

    public void GoToNext()
    { 
        Destroy(currentCar);
        spawnPoint = currentCar.transform;
        carIndex++;
        carIndex = carIndex == carList.Count ? 0 : carIndex;
        PlayerPrefs.SetInt("SavedCarIndex", carIndex);
        SpawnCar();
    }

    public void DriveCar()
    {
        selectionScreen.alpha = 0;
        selectionScreen.interactable = false;
        drivingScreen.alpha = 1;
        drivingScreen.interactable = true;
        carController.SetARObject(currentCar);
        if (OnTappedDrive != null)
            OnTappedDrive.Invoke(currentCar.transform);
    }

    public void OpenSelectionScreen()
    {
        if (OnTappedBack != null)
            OnTappedBack.Invoke(currentCar ? currentCar.transform : this.transform);
        selectionScreen.alpha = 1;
        selectionScreen.interactable = true;
        drivingScreen.alpha = 0;
        drivingScreen.interactable = false;
        Debug.Log("OpenedSelectionScreen", this);
    }
    
}
