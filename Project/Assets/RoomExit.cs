﻿using UnityEngine;
using System.Collections.Generic;

public class RoomExit : MonoBehaviour {

    public Vector2 displacement;
    public List<GameObject> playersTouching;
    private MainCameraControl mcc;
    public LayerMask WhatIsRoom;

    public void Init(Vector2 displacement) {
        this.displacement = displacement;
        mcc = GameObject.Find("Main Camera").GetComponent<MainCameraControl>();
    }

    void OnTriggerStay(Collider other) {
        if(playersTouching == null) {
            playersTouching = new List<GameObject>();
        }
        if(other.gameObject.CompareTag("Player") && !playersTouching.Contains(other.gameObject)) {
            playersTouching.Add(other.gameObject);
        }

        if(playersTouching.Count == GameObject.FindGameObjectsWithTag("Player").Length) {
            foreach(GameObject alpha in playersTouching) {
                alpha.transform.Translate(displacement, Space.World);
            }
            playersTouching.Clear();
            mcc.target += displacement * 2f;
            mcc.URHereMarkerTargetX += Mathf.RoundToInt(displacement.x * 0.2f);
            mcc.URHereMarkerTargetY -= Mathf.RoundToInt(displacement.y * 0.2f);
        }

        GameObject[] oldEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject alpha in oldEnemies) {
            Destroy(alpha);
        }

        Collider[] rooms = Physics.OverlapSphere(displacement * 2f, 2.0f, WhatIsRoom);
        Debug.Log("Came across " + rooms.Length + " room(s) after room x-fer.");
        foreach(Collider alpha in rooms) {
            Debug.Log("Came across " + alpha.gameObject.name + " after room x-fer.  Calling SpawnEnemies...");
            alpha.SendMessageUpwards("SpawnEnemies");
        }
    }

    void OnTriggerExit(Collider other) {
        if(playersTouching.Contains(other.gameObject)) {
            playersTouching.Remove(other.gameObject);
        }
    }
}
