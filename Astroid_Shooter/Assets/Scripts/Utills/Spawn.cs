using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Advertisements;

public class Spawn : MonoBehaviour {

	public GameObject rock_onePrefab;
	public GameObject rock_twoPrefab;
	public GameObject rock_threePrefab;
	public GameObject starPrefab;
	public GameObject cometPrefab;
	public GameObject basicEnemyPrefab;
	public GameObject trackingEnemyPrefab;
	public GameObject bigEnemyPrefab;

	public GameController controller;

    private bool boss = false;
    private int score;
    private float nextDrop = 1;
    private float dropInterval = 1;
    private void Start()
    {
        nextDrop = Time.time;
    }

    void Update () {
        score = controller.GetComponent<GameController>().score;
        if (Advertisement.isShowing == false)
        {
            if (Time.time >= nextDrop)
            {
                if (score <= 20)
                {
                    SpawnObject(rock_onePrefab, Quaternion.identity);
                }
                else if (score <= 50)
                {
                    SpawnObject(rock_onePrefab, Quaternion.identity);
                    SpawnObject(rock_twoPrefab, Quaternion.identity);
                }
                else if (score <= 100)
                {
                    SpawnObject(rock_onePrefab, Quaternion.identity);
                    SpawnObject(rock_twoPrefab, Quaternion.identity);
                    SpawnObject(rock_threePrefab, Quaternion.identity);
                }
                else if (score <= 300)
                {
                    SpawnObject(basicEnemyPrefab, Quaternion.Euler(0, 0, 180));
                }
                else if (score <= 1000)
                {
                    SpawnObject(trackingEnemyPrefab, Quaternion.Euler(0, 0, 180));
                }
                else if (score <= 2000)
                {
                    SpawnObject(basicEnemyPrefab, Quaternion.Euler(0, 0, 180));
                    SpawnObject(trackingEnemyPrefab, Quaternion.Euler(0, 0, 180));
                }
                else if (score <= 3000)
                {
                    if (!boss)
                    {
                        boss = true;
                        SpawnBigEnemy();
                    }

                }
                else
                {
                    dropInterval = .5f;
                    SpawnObject(basicEnemyPrefab, Quaternion.Euler(0, 0, 180));
                    SpawnObject(trackingEnemyPrefab, Quaternion.Euler(0, 0, 180));
                }
                SpawnObject(starPrefab, Quaternion.identity);
                nextDrop += dropInterval;
            }

            if (UnityEngine.Random.Range(0, 5000) == 2500)
            {
                SpawnComet();
            }
        }
        else
        {
            nextDrop = Time.time;
        }
	}

    void SpawnObject(GameObject ob, Quaternion rot)
    {
        Vector3 pos = GetXLocation();
        GameObject obj = ObjectPooler.GetPooledObject(ob);
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.SetActive(true);
    }
 
    private Vector3 GetXLocation()
    {
        return new Vector3(this.transform.position.x + UnityEngine.Random.Range(-5f, 5f),
            this.transform.position.y,
            -1);
    }

	void SpawnComet(){
		Vector3 pos = new Vector3 (this.transform.position.x - 12,
			this.transform.position.y - UnityEngine.Random.Range(1.5f,4.5f),
			-2);

        GameObject comet = ObjectPooler.GetPooledObject(cometPrefab);
        comet.transform.position = pos;
        comet.transform.rotation = transform.rotation;
        comet.SetActive(true);
    }
    
	void SpawnBigEnemy (){
		Vector3 pos = new Vector3 (this.transform.position.x,
			this.transform.position.y,
			-2);

        GameObject bigEnemy = ObjectPooler.GetPooledObject(bigEnemyPrefab);
        bigEnemy.transform.position = pos;
        bigEnemy.transform.rotation = transform.rotation;
        bigEnemy.SetActive(true);
    }
}

