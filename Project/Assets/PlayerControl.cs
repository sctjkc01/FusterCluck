using UnityEngine;
using System.Collections;

public enum Player {
    ONE, TWO, THREE, FOUR
}

public class PlayerControl : MonoBehaviour {

	private Vector3 force;
	public float speed;
	public float MAX_SPEED; 

    // Update is called once per frame
    void Update() {

		Move ();
        
    }

	void Move()
	{

		force = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

		force *= speed;

		force = Vector3.ClampMagnitude(force,speed);

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

				


	}
}
