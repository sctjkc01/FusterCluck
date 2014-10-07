using UnityEngine;
using System.Collections.Generic;

public class RoomExit : MonoBehaviour {

    public Vector2 displacement;
    public List<GameObject> playersTouching;

    public void Init(Vector2 displacement) {
        this.displacement = displacement;
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

            GameObject.Find("Main Camera").GetComponent<MainCameraControl>().target += displacement * 2f;
        }
    }

    void OnTriggerExit(Collider other) {
        if(playersTouching.Contains(other.gameObject)) {
            playersTouching.Remove(other.gameObject);
        }
    }
}
