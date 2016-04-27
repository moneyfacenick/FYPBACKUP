using UnityEngine;
using System.Collections;

public class Rain : MonoBehaviour {

    public AudioClip rain;

    private AudioSource source;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        source.playOnAwake = true;
	}
}
