using UnityEngine;
using System.Collections.Generic;

public class RoomExit : MonoBehaviour {

    public List<GameObject> playersTouching;

    void OnTriggerStay(Collider other) {
        if(playersTouching == null) {
            playersTouching = new List<GameObject>();
        }
        if(other.gameObject.CompareTag("Player")) {
            playersTouching.Add(other.gameObject);
        }
    }

    void OnTriggerLeave(Collider other) {
        if(playersTouching.Contains(other.gameObject)) {
            playersTouching.Remove(other.gameObject);
        }
    }
}
