using UnityEngine;
using System.Collections;

public class CameraLean : MonoBehaviour {

	public float LeanAmount;

	float rotationDamping = 6.0f;

	Waypoints waypointscript;
	Quaternion lean;
	// Use this for initialization
	void Start () {
		waypointscript = gameObject.GetComponentInParent<Waypoints> ();
	}
	
	// Update is called once per frame
	void Update () {

		// Check if the vehicle is currently turning
		if (waypointscript.GetUpdateWaypoint()) {
			Vector3 Direction1, Direction2;

			// Checking which direction the vehicle is turning
			Direction1 = waypointscript.transform.rotation.eulerAngles;
			Direction2 = waypointscript.GetObjectRotation().eulerAngles;

			if (Direction1.y < Direction2.y) {
				LeanRight ();
			}
			else if (Direction1.y > Direction2.y) {
				LeanLeft ();
			}
		}

		// If the vehicle is not turning
		else {
			lean = Quaternion.identity;
		}

		// Update the camera to lean in either direction or not lean
		UpdateRotate ();
	}

	void UpdateRotate()
	{
		// Rotate the camera to lean
		if (Quaternion.Angle (transform.localRotation, lean) > 2.5f) {
			Quaternion lerpedQuaternion = Quaternion.Lerp (transform.localRotation, lean, Time.deltaTime * rotationDamping);
			transform.localRotation = lerpedQuaternion;
		}

		// Check if the rotate is reached
		if ((Quaternion.Angle (transform.localRotation, lean) < 2.5f) && transform.localRotation != lean) {
			transform.localRotation = lean;
		}
	}

	void LeanRight()
	{
		lean = Quaternion.AngleAxis (LeanAmount, Vector3.forward);
	}

	void LeanLeft()
	{
		lean = Quaternion.AngleAxis (-LeanAmount, Vector3.forward);
	}
}
