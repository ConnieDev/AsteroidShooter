using UnityEngine;

public class DestroyShot : MonoBehaviour {

	public GameObject explosionPrefab;
    public int speed;
    
    private void OnEnable()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col){
        if (gameObject.tag == "Weapon")
        {
            if (col.tag == "Hazard" || col.tag == "Enemy" || col.tag == "EnemyWeapon"  || col.tag == "BigEnemy" || col.tag == "BigEnemyWeapon")
            {
                GameObject explosion = ObjectPooler.GetPooledObject(explosionPrefab);
                explosion.transform.position = transform.position;
                explosion.transform.rotation = transform.rotation;
                explosion.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
        else if (gameObject.tag == "EnemyWeapon")
        {
            if (col.tag == "Hazard" || col.tag == "Player" || col.tag == "Weapon")
            {
                GameObject explosion = ObjectPooler.GetPooledObject(explosionPrefab);
                explosion.transform.position = transform.position;
                explosion.transform.rotation = transform.rotation;
                explosion.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
    }
}
