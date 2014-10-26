using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    public static int spawns;

    public Transform enemyType1;
    public Transform enemyType2;
    private RoomManager rm;

    // Use this for initialization
    void Start() {
        rm = GameObject.Find("RoomManager").GetComponent<RoomManager>();
        spawns = 0;
    }

    void SpawnEnemies() {
        int qty = 0;
        spawns++;
        if(spawns < 3)
            qty = spawns; // If first or second spawn, spawn exactly one or two dudes, respectively.
        else
            qty = Random.Range(2, 5); // Otherwise, spawn 2, 3, or 4 dudes.
        for(int i = 0; i < qty; i++) {
            Vector3 location = transform.position + (Vector3)Random.insideUnitCircle * Random.Range(0f, 1.25f); // Spawn dude between 0 and 1.25 UU from center of room
            // Debug.Log("I want to see an enemy here: " + location);
            if(Random.Range(0.0f, 1.0f) >= 0.5) {
                Instantiate(enemyType1, location, Quaternion.identity);
            } else {
                Instantiate(enemyType2, location, Quaternion.identity);
            }
        }
    }
}
