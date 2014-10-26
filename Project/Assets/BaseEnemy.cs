using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {

    //Basic stuff
    public int hitPoints = 20;
    public int damage = 10;
    public float moveSpeed = 10;
    public float MaxSpeed = 10;

    public Vector3 playerPos;

    public float freezePeriod = 0.5f;

    // Use this for initialization
    public void Start() {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    void Update() {
        if(freezePeriod > 0) {
            freezePeriod -= Time.deltaTime;
        }
    }

}
