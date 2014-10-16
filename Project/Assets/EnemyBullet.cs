using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBullet : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
        rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            // When implemented: player.GetComponent<PlayerControl>().DealDamage(); 
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
