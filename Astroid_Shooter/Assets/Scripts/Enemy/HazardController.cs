using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardController : MonoBehaviour {

    public float speed;
    public int points;
    public int health;
    public Sprite Damaged;
    public Sprite Good;

    private float damage;
    private float damageRate;

    private void Start()
    {
        damageRate = GameController.controller.GetDamageRate();
    }

    void OnEnable()
    {
        if (this.name == "Rock01(Clone)" || this.name == "Rock01")
        {
            GetComponent<SpriteRenderer>().sprite = Good;
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Weapon")
        {
            damage += damageRate;
            if (damage >= health)
            {
                gameObject.SetActive(false);
                GameController.controller.AddPoints(points);
                damage = 0;
            }
            else
            {
                if (this.name == "Rock01(Clone)" || this.name == "Rock01")
                {
                    GetComponent<SpriteRenderer>().sprite = Damaged;
                }
            }
        }
        if (col.tag == "Player")
        {
            gameObject.SetActive(false);
            GameController.controller.AddPoints(points);
            damage = 0;
        }
    }
}
