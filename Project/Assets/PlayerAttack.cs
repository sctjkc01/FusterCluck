using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    public Component box;
    public GameObject player;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Debug.Log(this.gameObject.layer);
        Debug.Log(box.gameObject.activeSelf);

        transform.position = player.transform.position;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if(Mathf.Abs(horizontal) > 0.05 || Mathf.Abs(vertical) > 0.05) {
            Vector3 playerInput = new Vector3(horizontal, vertical);
            Debug.DrawLine(transform.position, transform.position + playerInput * 5f, Color.red);

            if(playerInput.x > 0) {
                transform.localScale = new Vector3(1f, -1f, 1f);
                playerInput.y *= -1;
            } else {
                transform.localScale = Vector3.one;
            }

            Debug.Log("" + Vector3.Angle(Vector3.up, playerInput));

            Quaternion rotation = Quaternion.identity;
            rotation.eulerAngles = new Vector3(0f, 0f, Vector3.Angle(Vector3.up, playerInput));
            transform.rotation = rotation;
        }
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag.Equals("Enemy")) {
            Destroy(collision.gameObject);
        }
    }

}
