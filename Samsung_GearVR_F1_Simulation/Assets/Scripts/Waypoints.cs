using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waypoints : MonoBehaviour {

	public Transform waypoint;

	public int waypointIndex;

	public float currentSpeed = 0.0f;

	List<Transform> waypoints;

	Vector3 direction;

	Quaternion objectRotation;

	Rigidbody body;

	float rotationDamping = 6.0f;
	float accelaration = 88.0f;
	float speedLimit = 1774.4f;

	bool updateWaypoint = true;

	// Use this for initialization
	void Start () {
		waypointIndex = 0;
		GetWayPoint ();

		body = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Move ();
	}

	void FixedUpdate() {
		// Rotate the camera to face the direction of the next waypoint
		UpdateRotate ();
	}

	public void OnTriggerEnter(Collider other)
	{
		// If collided with a waypoint then switch to the next waypoint
		if (other.CompareTag("Waypoint")) {
			ChangeNextWaypoint ();
		}
	}
		
	void UpdateRotate()
	{
		// If there is a waypoint
		if (waypoint) {

			// If the waypoint have been updated 
			if (updateWaypoint) {

				// Rotate the camera to view the next waypoint using interpolation
				body.MoveRotation (Quaternion.Lerp (transform.rotation, objectRotation, Time.deltaTime * rotationDamping));

				// If camera view is looking at the next waypoint stop updating
				if (Quaternion.Angle (transform.rotation, objectRotation) < 2.5f) {
					updateWaypoint = false;
				}
			}
		}
	}

	// Old
	void Move()
	{
		currentSpeed = currentSpeed + accelaration * accelaration;

        if (currentSpeed >= speedLimit)
        {
            currentSpeed = speedLimit;
        }

		Vector3 waypointPosition = waypoint.position;
		waypointPosition.y = transform.position.y;

		transform.position = Vector3.MoveTowards(transform.position, waypointPosition, Time.deltaTime * currentSpeed);
	}

	void ChangeNextWaypoint()
	{
		// Move to next waypoint
		waypointIndex++;

		// If have reach the last waypoint go back to the beginning
		if (waypointIndex >= waypoints.Count) {
			waypointIndex = 0;
		}

		// Update the waypoint to the next one
		waypoint = waypoints [waypointIndex];

		// Calculate the direction of this waypoint to the next one
		direction = waypoint.position - transform.position;
		direction.y = 0;
		direction.Normalize();

		// Calculate the Quaternion using the direction above
		objectRotation = Quaternion.LookRotation (direction);

		updateWaypoint = true;
	}

	void GetWayPoint()
	{
		Transform getwaypoints = waypoint.GetComponentInChildren<Transform>();
		waypoints = new List<Transform>();

		// Initing the waypoints into the list
		foreach (Transform findwaypoints in getwaypoints)
		{
			waypoints.Add(findwaypoints);
		}

		// Update the waypoint
		waypoint = waypoints [waypointIndex];

		// Calculate the direction of this waypoint to the next one
		direction = waypoint.position - transform.position;
		direction.y = 0;
		direction.Normalize();

		// Calculate the Quaternion using the direction above
		objectRotation = Quaternion.LookRotation (direction);
	}

	public bool GetUpdateWaypoint()
	{
		return updateWaypoint;
	}

	public Vector3 GetDirection()
	{
		return direction;
	}

	public Quaternion GetObjectRotation()
	{
		return objectRotation;
	}
}
