using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
public class GroundSpawner : MonoBehaviour {

    [SerializeField] GameObject groundTile;
    [SerializeField] GameObject groundTile2;
    Vector3 nextSpawnPoint;

    GameObject temp;

    GameObject current;
    public List<GameObject> listGroundTile;

    public void SpawnTile (bool spawnItems)
    {
        current = listGroundTile[0];
        
        temp = Instantiate(current, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;

        if (spawnItems) {
            temp.GetComponent<GroundTile>().SpawnObstacle();
            temp.GetComponent<GroundTile>().SpawnCoins();
        }
    }

    void Update() {
        
    }

    void Awake() {
        listGroundTile = new List<GameObject>();

        listGroundTile.Add(groundTile);
        listGroundTile.Add(groundTile2);
    }

    private void Start () {
        for (int i = 0; i < 15; i++) {
            if (i < 3) {
                SpawnTile(false);
            } else {
                SpawnTile(true);
            }
        }
    }
    
}