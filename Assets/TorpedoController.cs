using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Tyler Arseneault
 * Due: Feb 13, 2018
 * Description: Handles the movmement of the torpedo
 */
public class TorpedoController : MonoBehaviour {
    float speed;
    Rigidbody rbody;
    Transform shooter;
	/*
	 * Initialization
	 */
	void Start () {
        shooter = GameObject.Find("submarine").GetComponent<Transform>();
        speed = 5f;
        rbody = GetComponent<Rigidbody>();
        rbody.position = shooter.position;
        rbody.rotation = shooter.rotation;
	}
	
	/*
	 * Update the rigidbody once per frame
	 */
	void FixedUpdate () {
        rbody.velocity = transform.right * speed;
	}

    /*
     * Handles the collision of the torpedo
     */
    private void OnCollisionEnter(Collision collision)
    {
        //Destroy the boat and the torpedo on impact
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
