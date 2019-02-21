using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Shoot : MonoBehaviour {
    public GameObject LaserPrefab;

    private Vector3 laserOffset;
	private float fireRate;
    private float cooldownTimer = 0;

    private void Start()
    {
        if (this.gameObject.tag == "Player")
        {
            fireRate = GameController.controller.GetFireRate();
        }
        else
        {
            fireRate = 0.5f;
        }
        
    }

    void Update ()
    {
        if (Advertisement.isShowing == false)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                laserOffset = new Vector3(Random.Range(-0.2f, 0.2f), 1, 0);
                shoot();
                cooldownTimer = 1 / fireRate;
            }
        }
	}

    void shoot()
    {
        Vector3 offset = transform.rotation * laserOffset;
        GameObject laser = ObjectPooler.GetPooledObject(LaserPrefab);
        laser.transform.position = transform.position + offset;
        laser.transform.rotation = transform.rotation;
        laser.SetActive(true);
    }
}
