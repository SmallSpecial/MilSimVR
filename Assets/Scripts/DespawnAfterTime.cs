using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnAfterTime : MonoBehaviour {

	public float despawnTime;
	private float currentTimeLeft;

	// Use this for initialization
	void Start () {
		currentTimeLeft = despawnTime;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		currentTimeLeft = currentTimeLeft - Time.deltaTime;
		if (currentTimeLeft <= 0) {
			GameObject.Destroy(gameObject);
		}
	}
}
