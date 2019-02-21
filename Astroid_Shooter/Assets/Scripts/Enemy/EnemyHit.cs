using UnityEngine;

public class EnemyHit : MonoBehaviour {

    public float health;
    public int points;
	
    private float damageRate;
    private float damage;

    private void Start()
    {
        damage = 0;
        damageRate = GameController.controller.GetDamageRate();
    }

    void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Weapon"){
            damage += damageRate;
            if (health <= damage) {
				gameObject.SetActive(false);
                damage = 0;
                GameController.controller.AddPoints(points);
			} 
		}else if (col.tag == "Player"){
            gameObject.SetActive(false);
            damage = 0;
        }
	}

	void OnBecameInvisible(){
        gameObject.SetActive(false);
        damage = 0;
    }
}
