using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorSwitch : MonoBehaviour {

	public List<Color> ColorList;

	Renderer renderer;
	Material mat;

	int currentColorIndex;
	Color currentColor;
	Color nextColor;

	float time;
	float timeElapsed;
	public float timeIntervals;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<Renderer> ();
		mat = renderer.material;

		for(int i = 0; i < ColorList.Count; ++i)
		{
			ColorList [i] = ColorList [i];
		}

		currentColorIndex = 0;
		currentColor = ColorList [currentColorIndex];
		nextColor = ColorList [currentColorIndex + 1];
		time = 0;
	}

	// Update is called once per frame
	void Update () {

		timeElapsed += Time.deltaTime;

		if (timeElapsed < timeIntervals) {
			time += Time.deltaTime / timeIntervals;
			Color FinalColor = Color.Lerp (currentColor, nextColor, time);

			mat.SetColor ("_EmissionColor", FinalColor);
		}

		else {
			UpdateNextColor ();
			timeElapsed = 0;
			time = 0;
		}
	}

	void UpdateNextColor()
	{
		++currentColorIndex;

		if (currentColorIndex == ColorList.Count) {
			currentColorIndex = 0;
		}

		currentColor = ColorList [currentColorIndex];
		nextColor = ColorList [(currentColorIndex + 1) % ColorList.Count];
	}
}
