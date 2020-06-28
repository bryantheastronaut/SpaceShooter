using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] float health = 100f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3.5f;

    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 2f;
        

    private void Start() {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if (damageDealer) {
            ProcessHit(damageDealer);
        }
    }

    private void ProcessHit(DamageDealer damageDealer) {
        damageDealer.Hit();
        health -= damageDealer.GetDamage();
        if (health <= 0f) { Destroy(gameObject); }
    }

    private void Update() {
        CountdownAndShoot();
    }

    private void CountdownAndShoot() {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f) {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire() {
        var laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }

}
