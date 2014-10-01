using UnityEngine;
using System.Collections;

public enum Player {
    ONE, TWO, THREE, FOUR
}

public class PlayerControl : MonoBehaviour {

	private Vector2 forceDirection;
	public float speed;
	public float MAX_SPEED; 

    // Update is called once per frame
    void Update() {

		Move ();
        
    }

	void Move()
	{

		forceDirection = Vector2.zero;

		if (Input.GetAxis ("Vertical") > 0) {	
			forceDirection += (Vector2.up);
		}
		if (Input.GetAxis ("Horizontal") < 0) {
			forceDirection -= (Vector2.right);
		}
		if (Input.GetAxis ("Horizontal") > 0) {
			forceDirection += (Vector2.right);
		}
		if (Input.GetAxis ("Vertical") < 0) {
			forceDirection -= (Vector2.up);
		}
		forceDirection *= speed;

		Vector2 force = Vector2.ClampMagnitude(forceDirection, MAX_SPEED);

		rigidbody2D.AddForce(force, ForceMode2D.Impulse);

		rigidbody2D.velocity = Vector2.ClampMagnitude (rigidbody2D.velocity, MAX_SPEED);
				


	}
}
