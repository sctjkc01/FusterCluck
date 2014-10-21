using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    public Transform enemyType1;
    public Transform enemyType2;
    private RoomManager rm;

	// Use this for initialization
	void Start () {
        rm = GameObject.Find("RoomManager").GetComponent<RoomManager>();
	}

    void SpawnEnemies() {
        int qty = Random.Range(2, 5); //  2, 3, or 4.
        for(int i = 0; i < qty; i++) {
            Vector3 location = transform.position + new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), 0.0f);
            Debug.Log("I want to see an enemy here: " + location);
            if (Random.Range(0.0f, 1.0f) >= 0.5)
            {
                Instantiate(enemyType1, location, Quaternion.identity);
            }
            else
            {
                Instantiate(enemyType2, location, Quaternion.identity);
            }
        }
    }
}
