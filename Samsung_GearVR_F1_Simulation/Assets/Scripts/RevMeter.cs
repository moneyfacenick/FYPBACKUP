using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RevMeter : MonoBehaviour {

	public GameObject Vehicle;
	public float MaxRotate;
	Text gear, speed;
	RectTransform needleTransform;
	Quaternion needleRotate;
	Move MoveScript;

	// Use this for initialization
	void Start () {
		needleRotate = Quaternion.identity;

		MoveScript = Vehicle.GetComponent<Move> ();

		// Getting the transform of the needle object
		needleTransform = transform.FindChild ("Speedo").FindChild("Needle").GetComponent<RectTransform> ();

		// Getting the text ui of the gear and speed
		gear = transform.FindChild ("Gear").GetComponent<Text> ();
		speed = transform.FindChild ("Speed").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Calculating the angle to rotate based on RPM
		float rotateResult = MaxRotate * (MoveScript.GetRPM() / 15000);

		// Assigning the rotation to the needle
		needleRotate.eulerAngles = new Vector3 (0, 0, -rotateResult + 54);
		needleTransform.localRotation = needleRotate;

		// Updating the value display of the gear and speed
		gear.text = MoveScript.GetGear().ToString ();
		speed.text = Mathf.RoundToInt(Vehicle.GetComponent<Rigidbody> ().velocity.magnitude).ToString ();
	}
}
