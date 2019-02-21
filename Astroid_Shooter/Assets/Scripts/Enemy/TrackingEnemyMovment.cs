using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingEnemyMovment : MonoBehaviour {
	public float rotSpeed;
	public float speed;
	Transform player;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {		
		if (player == null) {
			GameObject go = GameObject.FindWithTag ("Player");
		if (go != null) {
				player = go.transform;
		}
		}
		if(player == null){
			return;
		}
		if (transform.position.y >= -2.2f) {
			Vector3 dir = player.position - transform.position;
			Vector3 pos = transform.position;
			Vector3 velocity = new Vector3 (0, speed * Time.deltaTime, 0);
			pos += transform.rotation * velocity;
			transform.position = pos;
			dir.Normalize ();

			float zAngle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90;

			Quaternion desiredRot = Quaternion.Euler (0, 0, zAngle);

			transform.rotation = Quaternion.RotateTowards (transform.rotation, desiredRot, rotSpeed * Time.deltaTime);
		} else {
			Vector3 pos = transform.position;
			Vector3 velocity = new Vector3 (0, speed * Time.deltaTime, 0);
			pos += transform.rotation * velocity;
			transform.position = pos;
		}
		
	}
}
