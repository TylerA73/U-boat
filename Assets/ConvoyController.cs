using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Tyler Arseneault
 * Due: Feb 13, 2018
 * Description: Handles the actions of the convoy boats
 */
public class ConvoyController : MonoBehaviour {
    public float speed;
    private Rigidbody rbody;

	/*
	 * Initialization
	 */
	void Start () {
        speed = 2f;
        rbody = GetComponent<Rigidbody>();
	}
	
    /*
     * Updates the rigidbody once per frame
     */
	void FixedUpdate () {
        rbody.velocity = transform.right * speed; 
	}
}
