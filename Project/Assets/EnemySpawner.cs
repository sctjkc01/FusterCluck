using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    private RoomManager rm;

	// Use this for initialization
	void Start () {
        rm = GameObject.Find("RoomManager").GetComponent<RoomManager>();
	}

    void SpawnEnemies() {
        int qty = Random.Range(2, 5); //  2, 3, or 4.

        for(int i = 0; i < qty; i++) {
            Vector3 location = transform.position + new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f), 0.0f);

        }
    }
}
