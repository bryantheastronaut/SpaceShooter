using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.5f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start() {
        SetUpMoveBoundary();
    }


    // Update is called once per frame
    void Update() {
        Move();
    }

    private void SetUpMoveBoundary() {
        Camera cam = Camera.main;
        xMin = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    private void Move() {
        var timeOffset = Time.deltaTime * moveSpeed;
        var deltaX = Input.GetAxis("Horizontal") * timeOffset;
        var deltaY = Input.GetAxis("Vertical") * timeOffset;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin + padding, xMax - padding);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin + padding, yMax - padding);
        transform.position = new Vector2(newXPos, newYPos);
    }
}
