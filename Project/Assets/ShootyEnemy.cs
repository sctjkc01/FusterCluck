using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BaseEnemy))]
[RequireComponent(typeof(Rigidbody))]
//Shooty Enemy. Effectively an octorok.
public class ShootyEnemy : MonoBehaviour
{
    public BaseEnemy enemy;
    public Transform bullet;

    // Use this for initialization
    void Start()
    {
        this.enemy = gameObject.GetComponent<BaseEnemy>();
        InvokeRepeating("ChangeDirection", 0.1f, 3);
        InvokeRepeating("LaunchBullet", 0.1f, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeDirection()
    {

        int direction = Random.Range(0, 3);
        //rigidbody.velocity = Vector3.zero;
        switch (direction)
        {
            case 0:
                transform.up = new Vector3(0, 1, 0);
                rigidbody.AddForce(0, 1, 0, ForceMode.Impulse);
                break;
            case 1:
				transform.up = new Vector3(0, -1, 0);
                rigidbody.AddForce(0, -1, 0, ForceMode.Impulse);
                break;
            case 2:
				transform.up = new Vector3(1, 0, 0);
                rigidbody.AddForce(1, 0, 0, ForceMode.Impulse);
                break;
            case 3:
			transform.up = new Vector3(-1, 0, 0);
                rigidbody.AddForce(-1, 0, 0, ForceMode.Impulse);
                break;
            default:
                break;
        }
    }



    void LaunchBullet()
    {
        Debug.Log("I wanna shoot a bullet.");
        Instantiate(bullet, transform.position + (transform.up * 2), transform.rotation);
    }
}
