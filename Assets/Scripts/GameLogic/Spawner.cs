using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
    [SerializeField]
    private List<MonsterSpawnProbEntry> spawnPrefabList;

    [SerializeField]
    private int initialSpawn = 20;

    [SerializeField]
    private Vector2 spawnDimension = new Vector2(100, 100);

    [SerializeField]
    private float spawnInterval = 2f;

    private float durSinceLastSpawn = 0;

    private void Spawn() {
        int rand = Random.Range(0, 100);
        Monster spawnPrefab = this.spawnPrefabList[0].monster;
        int selectCounter = 0;
        foreach (MonsterSpawnProbEntry entry in this.spawnPrefabList) {
            selectCounter += entry.probability;
            if (rand < selectCounter) {
                spawnPrefab = entry.monster;
                break;
            }
        }

        float xDim = this.spawnDimension.x / 2;
        float yDim = this.spawnDimension.y / 2;

        Vector3 randPos = new Vector3(Random.Range(-xDim, xDim), Random.Range(-yDim, yDim)) + this.transform.position;

        Monster spawned = GameObject.Instantiate<Monster>(spawnPrefab);
        spawned.transform.localPosition = randPos;
        spawned.transform.SetParent(this.transform);

        this.durSinceLastSpawn = 0;
    }

	// Use this for initialization
	void Start () {
	    for (int i = 0; i < this.initialSpawn; ++i) {
            this.Spawn();
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if (this.durSinceLastSpawn >= this.spawnInterval) {
            this.Spawn();
        }

        this.durSinceLastSpawn += Time.deltaTime;
	}

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(this.transform.position, this.spawnDimension);
    }
}
