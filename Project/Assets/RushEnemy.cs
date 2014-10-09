using UnityEngine;
using System.Collections;


[RequireComponent(typeof(BaseEnemy))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
public class RushEnemy : MonoBehaviour {

	public BaseEnemy enemy;
	Vector3 dv = Vector3.zero;
	CharacterController characterController;

	// Use this for initialization
	void Start () {
		this.enemy = gameObject.GetComponent<BaseEnemy>();
	}
	
	// Update is called once per frame
	void Update () {
		this.Move();
	}

	void Move(){
		dv = enemy.playerPos - transform.position;
		dv = dv.normalized * enemy.moveSpeed;
		dv -= rigidbody.velocity;
		dv.y = 0;

		rigidbody.velocity = dv;
	}
}
