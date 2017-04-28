using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipSnapPoints : MonoBehaviour {

    public GameObject playerCamera;


    private Transform originalTrans;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = playerCamera.transform.position + new Vector3(0.0f,-0.5f,0.0f);
        if ((playerCamera.transform.rotation.eulerAngles.x < 45) || (playerCamera.transform.rotation.eulerAngles.x > 90)) {
            print("rotating");
            transform.rotation = transform.rotation * Quaternion.AngleAxis(playerCamera.transform.rotation.y, Vector3.up);
        }
	}
}
