using UnityEngine;

public class GroundTile : MonoBehaviour {

    GroundSpawner groundSpawner;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject[] obstaclePrefab;

    private int obstacleSpawnIndex;

    private void Start () {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
	}

    private void OnTriggerExit (Collider other)
    {
        groundSpawner.SpawnTile(true);
        Destroy(gameObject, 2);
    }

    public void SpawnObstacle ()
    {
        int obstaclePrefabNumber = Random.Range(0, obstaclePrefab.Length);
        if (obstaclePrefabNumber == 0) {
            obstacleSpawnIndex = Random.Range(2, 5);
        } else if (obstaclePrefabNumber == obstaclePrefab.Length -1) {
            obstacleSpawnIndex = Random.Range(5, 8);
        }
        // Choose a random point to spawn the obstacle
      
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
        
        // Spawn the obstace at the position
        Instantiate(obstaclePrefab[obstaclePrefabNumber], spawnPoint.position, Quaternion.identity, transform);
    }


    public void SpawnCoins ()
    {
        int coinsToSpawn = 3;
        for (int i = 0; i < coinsToSpawn; i++) {
            GameObject temp = Instantiate(coinPrefab, transform);
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }
    }

    Vector3 GetRandomPointInCollider (Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );
        if (point != collider.ClosestPoint(point)) {
            point = GetRandomPointInCollider(collider);
        }

        point.y = 1;
        return point;
    }
}