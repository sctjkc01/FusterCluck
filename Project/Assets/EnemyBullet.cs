using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBullet : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
        rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
