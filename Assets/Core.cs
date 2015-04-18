using UnityEngine;
using System.Collections;

public class Core : MonoBehaviour {
    public GameObject enemyPrefab;
    public float      timeBetweenSpawns;
    private Vector2[] spawnPoints;
    private float spawnTimer;

	// Use this for initialization
	void Start () {
        spawnPoints = new Vector2[3];
        spawnTimer = 0.0f;
       
        Transform[] childTransforms = GetComponentsInChildren<Transform>();

        int numberOfSetSpawnPoints = 0;
        foreach (Transform childTransform in childTransforms)
        {
            if (childTransform.gameObject.tag == "SpawnPoint")
            {
                spawnPoints[numberOfSetSpawnPoints] = childTransform.position;
                Debug.Log("Set SpawnPosition to " + childTransform.position.x + "  " + childTransform.position.y);
                numberOfSetSpawnPoints++;
                if (numberOfSetSpawnPoints >= 3)
                    break;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= timeBetweenSpawns)
        {
            spawnTimer = 0.0f;

            for (int i = 0; i < 3; i++)
            {
                Instantiate(enemyPrefab, spawnPoints[i], Quaternion.identity);
                Debug.Log("Instantiated at " + spawnPoints[i].x + "  " + spawnPoints[i].y);
            }
        }
	}
}
