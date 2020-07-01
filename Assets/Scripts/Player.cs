using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Player")]
    [SerializeField] int health = 200;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.5f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 1f;
    [SerializeField] float projectileFiringPeriod = 0.15f;

    [Header("Audio")]
    [SerializeField] AudioClip shotSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float shotSoundVolume = 0.05f;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.25f;


    float xMin;
    float xMax;
    float yMin;
    float yMax;

    Coroutine firingCoroutine;

    // Start is called before the first frame update
    void Start() {
        SetUpMoveBoundary();
    }


    // Update is called once per frame
    void Update() {
        Move();
        if (Input.GetButtonDown("Fire1")) {
            firingCoroutine = StartCoroutine(RepeatFire());
        }
        if (Input.GetButtonUp("Fire1")) {
            StopCoroutine(firingCoroutine);
        }
    }

    private void Move() {
        // Time.deltaTime makes our movement framerate independant so movement is
        // identical on different machines
        var timeOffset = Time.deltaTime * moveSpeed;
        var deltaX = Input.GetAxis("Horizontal") * timeOffset;
        var deltaY = Input.GetAxis("Vertical") * timeOffset;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin + padding, xMax - padding);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin + padding, yMax - padding);
        transform.position = new Vector2(newXPos, newYPos);
    }

    IEnumerator RepeatFire() {
        // once this starts, just keep it going
        while (true) {
            // Quaternion.identity means no rotation
            var laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shotSound, Camera.main.transform.position, shotSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if (damageDealer) {
            damageDealer.Hit();
            health -= damageDealer.GetDamage();
            if (health <= 0) { Die(); }
        }
    }

    private void Die() {
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        Destroy(gameObject);
        FindObjectOfType<Level>().OnGameOver();
    }

    public int GetHealth() { return health; }

    private void SetUpMoveBoundary() {
        Camera cam = Camera.main;
        xMin = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

}
