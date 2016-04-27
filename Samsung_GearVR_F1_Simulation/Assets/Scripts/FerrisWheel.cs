using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FerrisWheel : MonoBehaviour {

    // List to store childs of the ferris wheel[capsules, wheel, base]
    private List<Transform> ferrisWheel;
    public int Speed;
	// Use this for initialization
	void Start () {
        GetObjects();
	}
	
	// Update is called once per frame
	void Update () {
        SpinWheel();
	}

    void GetObjects()
    {
        // Gets all the child and store it in the list
        Transform getAllObjects = transform;
        ferrisWheel = new List<Transform>();
        
        foreach (Transform findObjects in getAllObjects)
        {
            ferrisWheel.Add(findObjects);
        }

    }

    void SpinWheel()
    {
        for(int i = 0; i < ferrisWheel.Count; i++)
        {
            if(ferrisWheel[i].transform.name == "Wheel")
                ferrisWheel[i].transform.Rotate(Vector3.right * Time.deltaTime * Speed);
            if(ferrisWheel[i].transform.name == "Capsule")
            {   
                // Spin the wheel
                ferrisWheel[i].transform.Rotate(Vector3.right * Time.deltaTime * Speed);
                foreach (Transform findCapsules in ferrisWheel[i])
                {
                    // Spin all Capsules in the opp direction for it to look like its not spinning
                    findCapsules.transform.Rotate(Vector3.left * Time.deltaTime * Speed);
                }
            }
        }
    }

}
