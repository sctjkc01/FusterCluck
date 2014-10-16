using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {

	//Basic stuff
	public int hitPoints = 20;
	public int damage = 10;
	public float moveSpeed = 10;
	public float MaxSpeed = 10;
	


	

	// Use this for initialization
	public void Start () {
		playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
	}

	public void update () 
	{

	}

}
