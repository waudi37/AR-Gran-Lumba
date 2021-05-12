 using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ARSceneController : MonoBehaviour
{
    private GameObject prefab, SpawnObject, go;
    public GameObject LamborghiniPrefab;
    public GameObject[] objectPrefabs;
    public int selectedobjectPrefabs;
    public Text displayStatus;
    public Transform spawnLocation, parentGO;
    public GameObject joystickCanvas;

    private bool ModelAlive;

    private int currentIndex;
    private const float ModelRotation = 180.0f;

    private void Start()
    {
        currentIndex = 0;
        ModelAlive = false;
        displayStatus.text = "";
        prefab = LamborghiniPrefab;
        joystickCanvas.SetActive(false);
    }

    private void Update()
    {
        _QuitOnConnectionErrors();
        TouchOnModel();
        joystickCanvas.SetActive(true);
    }

    private void _QuitOnConnectionErrors()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void CheckAlive()
    {
        displayStatus.text = prefab.name;

        if (!ModelAlive)
        {
            ModelAlive = true;
            Invoke("SpawnModel", 0.1f);

        }
        else
        {
            Destroy(SpawnObject);
            Invoke("SpawnModel", 0.1f);
        }
    }

    private void SpawnModel() {
        SpawnObject = Instantiate(prefab, spawnLocation.position, spawnLocation.rotation);
        SpawnObject.transform.parent = parentGO.transform;
        switch (prefab.name)
        {
            case "Lamborghini Aventador":
                SpawnObject.transform.name = "Lamborghini Aventador";
                SpawnObject.transform.Rotate(0, 90, 0, Space.Self);
                break;

            case "Chevrolet Touring 666":
                SpawnObject.transform.name = "Chevrolet Touring 666";
                SpawnObject.transform.Rotate(0, 90, 0, Space.Self);
                break;

            case "Dodge Challenger":
                SpawnObject.transform.name = "Dodge Challenger";
                SpawnObject.transform.Rotate(0, 90, 0, Space.Self);
                break;

            case "Lamborghini Aventador Police":
                SpawnObject.transform.name = "Lamborghini Aventador Police";
                SpawnObject.transform.Rotate(0, 90, 0, Space.Self);
                break;

            case "Subaru Imprezza":
                SpawnObject.transform.name = "Subaru Imprezza";
                SpawnObject.transform.Rotate(0, 90, 0, Space.Self);
                break; 

        }
        displayStatus.text = SpawnObject.transform.name;
    }

    private void TouchOnModel()
    {
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        RaycastHit touchHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

        if (Physics.Raycast(ray, out touchHit))
        {
            if (touchHit.collider != null)
            {
                go = touchHit.collider.gameObject;
                displayStatus.text = go.name;
            }
        }
    }


    public void SpawnCar()
    {
        prefab = LamborghiniPrefab;
        CheckAlive();
    }

    public void PreviousModels()
    {
        currentIndex--;
        currentIndex = currentIndex < 0 ? objectPrefabs.Length-1 : currentIndex;
        Debug.Log("how many here + " + objectPrefabs.Length);
        prefab = objectPrefabs[currentIndex];
        CheckAlive();
    }
    public void NextModels()
    {
        currentIndex++;
        currentIndex = currentIndex > objectPrefabs.Length-1 ? 0 : currentIndex;
        prefab = objectPrefabs[currentIndex];
        CheckAlive();
    }

}