using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMovement : MonoBehaviour {

    public GameObject playerCamera;
    public GameObject cameraRig;
    public Animator animController;
    public float velocityLimit = 0.0001f;
    public float multiplier = 5000f;

    private Vector3 lastPosition;
    private Vector3 currentPosition;

    public LayerMask targetLayers;

    private void Start() {
        lastPosition = playerCamera.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate() {
        lastPosition = currentPosition;
        currentPosition = playerCamera.transform.position;
        Vector3 velocity = (currentPosition - lastPosition) * Time.deltaTime;
        velocity.y = 0.0f;
        float magnitude = velocity.magnitude;
        velocity.Normalize();
        Vector3 cameraLook = playerCamera.transform.forward;
        cameraLook.y = 0;
        cameraLook.Normalize();
        //print(Vector3.Angle(cameraLook, velocity));
        if (Vector3.Angle(cameraLook, velocity) < 90 && magnitude > 0.0001f) {
            transform.rotation = Quaternion.LookRotation(velocity, Vector3.up);
        }
        else {
            transform.rotation = Quaternion.LookRotation(cameraLook, Vector3.up);
        }
        
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, -Vector3.up, out hit, float.PositiveInfinity, targetLayers)) {
            transform.position = hit.point;
        }
        
        if (magnitude > velocityLimit) {
            animController.SetFloat("speed", magnitude * multiplier);
            animController.SetBool("running", true);
        } else {
            animController.SetFloat("speed", 1f);
            animController.SetBool("running", false);
        }
    }
}