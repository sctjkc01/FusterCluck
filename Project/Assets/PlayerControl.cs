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

	public Animator animator;

	public int health; 

	void Start(){
		animator = this.GetComponent<Animator>();
	}

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
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");

		Debug.Log ("H: " + horizontal + ", V: " + vertical);

		//Animation stuff
        if(Mathf.Abs(horizontal) > 0.05 || Mathf.Abs(vertical) > 0.05) {
            animator.SetBool("Walking", true);
            animator.SetFloat("DirX", horizontal);
            animator.SetFloat("DirY", vertical);
        } else {
            animator.SetBool("Walking", false);
        }


		force = new Vector3(horizontal, vertical, 0);

		force *= speed;

		force = Vector3.ClampMagnitude(force,speed);

		rigidbody.velocity = force;
		

	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag.Equals ("Enemy") || collision.gameObject.tag.Equals ("Bullet")) {
			health --;
		}
		if (health <= 0) {
			//do death stuff
		}
	}
}
