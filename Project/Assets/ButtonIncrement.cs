using UnityEngine;
using System.Collections.Generic;

public class ButtonIncrement : MonoBehaviour {

    public RoomControl rc;
    public List<GameObject> playersTouching;
    public float IncrementDelay = 0.5f;
    private float timer = 0f;

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.name.Equals("PlayerAttackBox")) {
            Debug.Log("AAAAH!");
        }
    }
}
