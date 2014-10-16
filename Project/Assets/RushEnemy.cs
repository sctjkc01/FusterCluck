using UnityEngine;
using System.Collections;


[RequireComponent(typeof(BaseEnemy))]
[RequireComponent(typeof(Rigidbody))]
public class RushEnemy : MonoBehaviour {

	public BaseEnemy enemy;
	Vector3 dv = Vector3.zero;

	// Use this for initialization
	void Start () {
		this.enemy = gameObject.GetComponent<BaseEnemy>();
	}
	
	// Update is called once per frame
	void Update () {
		enemy.playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
		this.Move();
	}

	void Move(){
		dv = enemy.playerPos - transform.position;
		dv = dv.normalized * enemy.moveSpeed;
		dv -= rigidbody.velocity;
		dv.z = 0;

		rigidbody.AddForce(dv, ForceMode.Impulse);
	}
}
