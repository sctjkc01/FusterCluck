﻿using UnityEngine;
using System.Collections;

public enum Player {
    ONE, TWO, THREE, FOUR
}

public class PlayerControl : MonoBehaviour {

	private Vector3 force;
	public float speed;
	public float MAX_SPEED;
	private Vector3 _referenceSpeed = Vector3.zero;

	public float rotateSpeed = 30f;

    // Update is called once per frame
    void Update() {

		Move ();
        
    }

	void Move()
	{

		force = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

		force *= speed;

		force = Vector3.ClampMagnitude(force,speed);

		rigidbody.velocity = force;

		/*
		if (force != Vector3.zero)
		{
			this.rigidbody.AddForce(force, ForceMode.VelocityChange);
		
			
		}
		else
		{
			this.rigidbody.velocity = Vector3.zero;
		}

		if (this.rigidbody.velocity.magnitude > MAX_SPEED)
		{
			this.rigidbody.velocity = Vector3.ClampMagnitude(this.rigidbody.velocity, MAX_SPEED);
		}
		 */


		//this.gameObject.transform.up = Vector3.SmoothDamp(this.gameObject.transform.up, rigidbody.velocity.normalized, ref _refranceSpeed, rotateSpeed);

		Debug.Log(rigidbody.velocity.normalized);

		if (rigidbody.velocity.normalized != Vector3.zero)
		{
			this.gameObject.transform.up = rigidbody.velocity.normalized;
		}
	}
}
