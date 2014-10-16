using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	public Component box;
	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (this.gameObject.layer);
		Debug.Log (box.gameObject.activeSelf);
		Vector3 pos = new Vector3 (player.transform.position.x, player.transform.position.y + .5f, player.transform.position.z);
		this.transform.position = pos;
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == 12)
		{
			Destroy(collision.gameObject);
		}
		
	}

}
