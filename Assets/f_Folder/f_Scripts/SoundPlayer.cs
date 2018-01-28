using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playSound(AudioClip sound)
    {
        transform.GetComponent<AudioSource>().clip = sound;
        transform.GetComponent<AudioSource>().Play();
    }
}
