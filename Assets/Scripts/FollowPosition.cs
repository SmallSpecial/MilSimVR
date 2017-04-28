using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour {

    public GameObject objectToFollow;
    public Vector3 offset;

    private Quaternion tempQuaternion;

	void FixedUpdate () {
        tempQuaternion = objectToFollow.transform.rotation * Quaternion.Euler(offset);
        transform.rotation = tempQuaternion;
    }
}
