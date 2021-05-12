using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollectibleController : MonoBehaviour
{
    public int scoreValue;
    public Action<CollectibleController> onCollected;
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider col)
    {
        Debug.Log($"entered trigger {col.gameObject.name}");
        if (col.gameObject.tag == "CarList")
        {
            onCollected?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
