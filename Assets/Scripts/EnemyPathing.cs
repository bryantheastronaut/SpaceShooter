using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float moveSpeed = 1.25f;

    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start() {
        transform.position = waypoints[waypointIndex].transform.position;
        waypointIndex++;
    }

    private void Move() {
        if (waypointIndex <= waypoints.Count - 1) {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = Time.deltaTime * moveSpeed;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            if (transform.position == waypoints[waypointIndex].transform.position) {
                waypointIndex++;
            }
        }  else {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update() {
        Move();
    }
}
