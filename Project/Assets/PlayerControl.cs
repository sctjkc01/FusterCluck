using UnityEngine;
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
    public LayerMask WhatIsButton;

	public Animator animator;

	public int health;

    public Vector3 facing;

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

        if(Input.GetButtonDown("Fire1")) {
            float horizontal = facing.x;
            float vertical = facing.y;
            Debug.DrawLine(transform.position, transform.position + new Vector3(horizontal, vertical) * 0.5f);
            Collider[] test = Physics.OverlapSphere (transform.position + new Vector3(horizontal, vertical) * 0.5f, 0.5f);
            foreach(Collider t in test) {
                if(t.GetComponent<ButtonIncrement>() != null) {
                    t.GetComponent<ButtonIncrement>().rc.IncrementNumber();
                }
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

		//Animation stuff
        if(Mathf.Abs(horizontal) > 0.05 || Mathf.Abs(vertical) > 0.05) {
            facing = new Vector3(horizontal, vertical);
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
			RoomManager.GameOverRef.GameDone(false);
		}
	}
}
