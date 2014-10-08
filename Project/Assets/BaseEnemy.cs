﻿using UnityEngine;
using System.Collections;

public abstract class BaseEnemy : MonoBehaviour {

	//Basic stuff
	public int hitPoints = 20;
	public int damage = 10;
	public int moveSpeed = 10;

	public Vector3 playerPos;

	// Use this for initialization
	void Start () {

	}
}