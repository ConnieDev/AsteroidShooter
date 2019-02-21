using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometMovement : MonoBehaviour {

    public float speed;

    private void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
