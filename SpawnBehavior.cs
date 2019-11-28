using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehavior : MonoBehaviour
{

    public Transform[] SpawnPoints;
    public GameObject[] Enemy;
    public GameObject Boss;
    public float SpawnTime;
    public float SpawnDelay;
    private int SpawnCount;
    private int SpawnLevel;
    private GameObject Player;

    void Awake() {
      Player = GameObject.Find("Player");
      InvokeRepeating("AddEnemy", SpawnTime, SpawnDelay);
    }

    void AddEnemy() {
      if (Player != null) {
        int spawnPointIndex = Random.Range(0, SpawnPoints.Length);
        int enemyIndex = Random.Range(0, Enemy.Length);
          Instantiate (Enemy[enemyIndex], SpawnPoints[spawnPointIndex].position, SpawnPoints[spawnPointIndex].rotation);
          SpawnCount += 1;
      }
    }

    void Update() {
      if (SpawnCount >= 30) {
        int spawnPointIndex = Random.Range(0, SpawnPoints.Length);
        Instantiate (Boss, SpawnPoints[spawnPointIndex].position, SpawnPoints[spawnPointIndex].rotation);
        Destroy(this.gameObject);
      }
    }

}
