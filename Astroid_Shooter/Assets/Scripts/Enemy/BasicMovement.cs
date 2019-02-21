using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour {

    public int speed;

	void OnEnable() {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
    }

    void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }
}
