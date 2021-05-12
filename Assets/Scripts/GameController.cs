using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private CarListController carListController;
    [SerializeField] private int maxGems = 1;
    [SerializeField] GameObject gemPrefab;
    [SerializeField] TMPro.TMP_Text scoreText;
    private List<GameObject> currentGems = new List<GameObject>();
    private int score = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpawnPoint(Transform spawnPoint)
    {
        score = 0;
        while (currentGems.Count < maxGems)
        {
            GenerateCollectible(carListController.currentCar.transform);
        }
    }

    public void Cleanup(Transform spawnPoint)
    {
        foreach (var gem in currentGems)
        {
            Destroy(gem);
        }
        currentGems.Clear();
        Debug.Log("Cleaning up", this);
        SetScore(0);
    }

    private void GenerateCollectible(Transform spawnPoint)
    {
        var gemPosition = GenerateGemPosition(spawnPoint.position);
        GameObject item = Instantiate(gemPrefab, gemPosition, Quaternion.identity);
        currentGems.Add(item);
        item.GetComponentInChildren<CollectibleController>().onCollected += ScoreUp;
    }

    private void ScoreUp(CollectibleController obj)
    {
        SetScore(score + obj.scoreValue);
        currentGems.Remove(obj.gameObject);
        GenerateCollectible(carListController.currentCar.transform);
    }

    private void SetScore(int scoreValue)
    {
        score = scoreValue;
        scoreText.text = "Score: " + score;
    }

    private Vector3 GenerateGemPosition(Vector3 position)
    {
        float randomDistance = Random.value * 1 + 0.5f; // random range is between [0 and 1], constant is 0.5
        Quaternion randomDirection = Quaternion.AngleAxis(Random.value * 360, Vector3.up);

        return position + (randomDirection * Vector3.forward) * randomDistance;
    }

    //public void GameOver() {
        //GameOverText.SetActive(true);
        //PlayfabManager.SendLeaderboard(maxPlatform);


}
