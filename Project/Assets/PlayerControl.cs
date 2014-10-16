﻿using UnityEngine;
using System.Collections;

public enum Player {
    ONE, TWO, THREE, FOUR
}

public class PlayerControl : MonoBehaviour {

	private Vector3 force;
	public float speed;
	public float ATTACKTIME;

	private float _attackTimer;

	public GameObject attackBox;

	public Animator animator = this.GetComponent<playerAnimator>();


    // Update is called once per frame
    void Update() {

		Attack();

		Move ();
        
    }

	void Attack()
	{
		if (!attackBox.activeSelf)
		{
			//Debug.Log("boop");
			if (Input.GetButton("Fire1"))
			{
				attackBox.SetActive(true);
				_attackTimer = ATTACKTIME;
			}
		}


		if(_attackTimer <= 0)
		{
			attackBox.SetActive(false);
		}
		else
		{
			_attackTimer -= Time.deltaTime;
		}


	}

	void Move()
	{
		var horizontal = Input.GetAxisRaw("Horizontal");
		var vertical = Input.GetAxisRaw("Vertical");

		Debug.Log("moving");

		//Animation stuff
		if(horizontal > 0)
		{
			Debug.Log("Moving right");
			if(vertical > 0)
			{
				animator.SetInteger("Direction", 1);
			}
			else if(vertical < 0)
			{
				animator.SetInteger("Direction", 3);
			}
			else
			{
				animator.SetInteger("Direction", 2);
			}
		}
		else if (horizontal < 0)
		{
			if(vertical > 0)
			{
				animator.SetInteger("Direction", 7);
			}
			else if(vertical < 0)
			{
				animator.SetInteger("Direction", 5);
			}
			else
			{
				animator.SetInteger("Direction", 6);
			}
		}
		else
		{
			if(vertical > 0)
			{
				animator.SetInteger("Direction", 0);
			}
			else if(vertical < 0)
			{
				animator.SetInteger("Direction", 4);
			}
			else
			{
				animator.SetInteger("Direction", 0);
			}
		}


		force = new Vector3(horizontal, vertical, 0);

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


		//Debug.Log(rigidbody.velocity.normalized);
		
		if (rigidbody.velocity.normalized != Vector3.zero)
		{
			this.gameObject.transform.up = rigidbody.velocity.normalized;
		}
	}
}
