using UnityEngine;
using System.Collections;

public class RushEnemy : BaseEnemy {

	Vector3 dv = Vector3.zero;
	CharacterController characterController;

	// Use this for initialization
	void Start () {
		characterController = gameObject.GetComponent<CharacterController>();
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		this.Move();
	}

	void Move(){
		dv = playerPos - transform.position;
		dv = dv.normalized * moveSpeed;
		dv -= characterController.velocity;
		dv.y = 0;
	}
}
