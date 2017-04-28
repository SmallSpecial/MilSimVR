using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPositionAndRotation : MonoBehaviour {

    public Transform objectToFollow;
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = objectToFollow.position;
        transform.rotation = objectToFollow.rotation;
	}
}
