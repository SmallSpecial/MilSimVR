using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetShot : MonoBehaviour {

	private float yThreshold = -10.0f;
	private float velocity = -6.0f;

    public bool haveIBeenShot = false;


	void Update() {
		if (transform.position.y < yThreshold) {
			GameObject.Destroy(gameObject);
		}
	}

	public void Shot() {
		GetComponent<Rigidbody>().velocity = new Vector3(0.0f, velocity, 0.0f);
        haveIBeenShot = true;
	}
}
