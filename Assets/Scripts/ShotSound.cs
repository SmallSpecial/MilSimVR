using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSound : MonoBehaviour {

	public AudioClip shotSound;
	private AudioSource audioSource;

	void Start () {
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = shotSound;
	}

	public void Play() {
		audioSource.PlayOneShot(shotSound);
	}
}
