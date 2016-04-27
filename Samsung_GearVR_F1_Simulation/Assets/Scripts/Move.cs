using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move : MonoBehaviour {

	public List<float> speedCap;

	public float constantAccelerate;
	float rpm;
	float brakeDecceleration;

	int currentGear;

	Waypoints waypointScript;

	Rigidbody body;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody> ();
		waypointScript = GetComponent<Waypoints> ();

		currentGear = 1;

		brakeDecceleration = 49;

		for (int i = 0; i < speedCap.Count; ++i) {
			speedCap [i] = speedCap [i];
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetAxis ("Vertical") > 0) {
			Accelerate ();
			body.drag = 0.0001f;
		} else if (Input.GetAxis ("Vertical") < 0) {
			Decelerate ();
			body.drag = 0.0001f;
		} else if (Input.GetKey (KeyCode.Space)) {
			Brake ();
		} else {
			body.drag = 0.2f;
		}

		// Calculate the current rpm based on the vehicle speed and gear
		rpm = (body.velocity.magnitude / speedCap [currentGear - 1]) * 15000;

		// Shift gears based on rpm
		ShiftGears ();
	}

	void Accelerate() 
	{
		if (body.velocity.magnitude < 340) {

			// Calculate the amount of acceleration based on current rpm
			float finalAcceleration = (constantAccelerate + 0.1f) - ((rpm / 15000) * constantAccelerate);

			// Calculate the new velocity using the old velocity plus current frame's acceleration
			Vector3 newVelocity = (waypointScript.GetDirection() * body.velocity.magnitude) + ((waypointScript.GetDirection() * finalAcceleration) * Time.deltaTime);

			// Assign the new velocity to the rigidbody
			body.velocity = newVelocity;
		}
	}

	void Decelerate() 
	{
		// Calculate the amount of acceleration based on current rpm
		float finalAcceleration = -((constantAccelerate + 0.1f) - ((rpm / 15000) * constantAccelerate));

		// Calculate the new velocity using the old velocity plus current frame's acceleration
		Vector3 newVelocity = (waypointScript.GetDirection() * body.velocity.magnitude) - ((waypointScript.GetDirection() * finalAcceleration) * Time.deltaTime);

		// Assign the new velocity to the rigidbody
		body.velocity = newVelocity;
	}

	void Brake()
	{
		float currentSpeed = body.velocity.magnitude;

		// Checking if the vehicle have stopped if not then deccelerate the vehicle
		if (currentSpeed != 0 && currentSpeed >= 1f) {
			Vector3 newVelocity = (waypointScript.GetDirection() * currentSpeed) - ((waypointScript.GetDirection() * brakeDecceleration) * Time.deltaTime);
			body.velocity = newVelocity;
		}

		// Check if speed if reaching 0
		if (currentSpeed < 1f && currentSpeed > 0) {
			body.velocity = Vector3.zero;
			body.angularVelocity = Vector3.zero;
		}
	}

	void ShiftGears()
	{
		// If rpm have reach or is over rev limit and there are still gears left to shiftup 
		if (rpm >= 15000 && currentGear != speedCap.Count) {
			++currentGear;
			rpm = (body.velocity.magnitude / speedCap [currentGear - 1]) * 15000;
		}

		// If rpm have reach or is over rev limit cap the rpm
		else if (rpm > 15000 && currentGear == speedCap.Count) {
			rpm = 15000;
		}

		// If rpm is low enough to shiftdown
		else if (rpm < 13000 && currentGear != 1) {
			--currentGear;
			rpm = (body.velocity.magnitude / speedCap [currentGear - 1]) * 15000;
		}
	}

	public float GetRPM(){
		return rpm;
	}

	public int GetGear(){
		return currentGear;
	}
}
