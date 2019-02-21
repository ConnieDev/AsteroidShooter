using UnityEngine;

public class PlayerHit : MonoBehaviour {

	private int health;

	void Start(){
        health = 3;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Hazard" || col.tag == "Enemy" || col.tag == "EnemyWeapon" || col.tag == "BigEnemyWeapon")
        {
			if (health == 1) {
                GameController.controller.PlayerWasKilled();
			} else {
                health--;
                GameController.controller.PlayerWasHit(health);
            }
		}
	}
	
}
