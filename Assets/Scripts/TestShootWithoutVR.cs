using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShootWithoutVR : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.S)) {
			print("Shooting");
			GetComponent<Shoot>().FireBullet();
		}
	}

}
