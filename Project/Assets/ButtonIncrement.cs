using UnityEngine;
using System.Collections.Generic;

public class ButtonIncrement : MonoBehaviour {

    public RoomControl rc;
    public List<GameObject> playersTouching;
    public float IncrementDelay = 0.5f;
    private float timer = 0f;

    void OnTriggerStay(Collider other) {
        if(playersTouching == null) {
            playersTouching = new List<GameObject>();
        }
        if(other.gameObject.CompareTag("Player") && !playersTouching.Contains(other.gameObject)) {
            playersTouching.Add(other.gameObject);
        }
        
        if(playersTouching.Count > 0) {
            if(timer > IncrementDelay) {
                timer -= IncrementDelay;
                rc.IncrementNumber();
            } else {
                timer += Time.deltaTime;
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if(playersTouching.Contains(other.gameObject)) {
            playersTouching.Remove(other.gameObject);
        }
    }
}
