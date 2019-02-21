using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemyMovement : MonoBehaviour {

	public float speed;
	public bool right = true;

	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void Update () {
		
    	if(transform.position.x > 2){
			right = false;
		}
		if (transform.position.x < -2){
			right = true;
		}
		if (transform.position.y > 3.5f) {
			Vector3 pos = transform.position;
			Vector3 velocity = new Vector3 (0, speed * Time.deltaTime, 0);
			pos += transform.rotation * velocity;
			transform.position = pos;
		} else {
			if (right){
				speed = 2;
			}
			if (!right){
				speed = -2;
			}
			if(transform.position.x > -3 && transform.position.x < 3){
				Vector3 pos = transform.position;
				Vector3 velocity = new Vector3 (speed * Time.deltaTime, 0, 0);
				pos += transform.rotation * velocity;
				transform.position = pos;
			}
		}
		
	}
}
