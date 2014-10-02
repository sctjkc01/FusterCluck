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

		force = Vector3.zero;

		if (Input.GetAxis ("Vertical") > 0) {	
			force += (Vector3.up);
		}
		if (Input.GetAxis ("Horizontal") < 0) {
			force -= (Vector3.right);
		}
		if (Input.GetAxis ("Horizontal") > 0) {
			force += (Vector3.right);
		}
		if (Input.GetAxis ("Vertical") < 0) {
			force -= (Vector3.up);
		}

		force *= speed;

		force = Vector3.ClampMagnitude(force,MAX_SPEED);

		this.gameObject.transform.Translate(force);

				


	}
}
