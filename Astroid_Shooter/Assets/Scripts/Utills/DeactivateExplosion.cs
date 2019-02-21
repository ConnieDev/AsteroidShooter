using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateExplosion : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
        Invoke("Deactivate", .5f);
	}
	 private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
