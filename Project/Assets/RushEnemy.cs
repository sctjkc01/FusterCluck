using UnityEngine;
using System.Collections;


[RequireComponent(typeof(BaseEnemy))]
[RequireComponent(typeof(Rigidbody))]
public class RushEnemy : MonoBehaviour {

	public BaseEnemy enemy;
	Vector3 dv = Vector3.zero;
	public GameObject player;



	// Use this for initialization
	void Start () {
		//this.enemy = gameObject.GetComponent<BaseEnemy>();
		this.player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		this.Move();
	}

	void Move(){



		dv =  this.player.transform.position -  this.gameObject.transform.position ;
		dv = dv.normalized * enemy.moveSpeed;
		//dv -= rigidbody.velocity;
		dv.z = 0;

		rigidbody.velocity = dv;

		/*
		rigidbody.AddForce(dv, ForceMode.Impulse);

		if (this.rigidbody.velocity.magnitude > enemy.MaxSpeed)
		{
			this.rigidbody.velocity = Vector3.ClampMagnitude(this.rigidbody.velocity, enemy.MaxSpeed);
		}
		*/

	}
}
