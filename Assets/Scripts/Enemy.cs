using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Basic info")]
    [SerializeField] float health = 100f;
    [SerializeField] int pointValue = 100;

    [Header("Shot info")]
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3.5f;
    [SerializeField] float projectileSpeed = 2f;
    [SerializeField] float destroyEffectAfter = 1f;

    [Header("Prefabs")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject explosionPrefab;

    [Header("Audio")]
    [SerializeField] AudioClip shotSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float shotSoundVolume = 0.1f;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.2f;

    float shotCounter;


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
        if (health <= 0f) {
            Explode();
        }
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
        AudioSource.PlayClipAtPoint(shotSound, Camera.main.transform.position, shotSoundVolume);
    }

    private void Explode() {
        FindObjectOfType<GameSession>().UpdateScore(pointValue);
        Destroy(gameObject);
        var explodeEffect = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explodeEffect, destroyEffectAfter);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);

    }


}
