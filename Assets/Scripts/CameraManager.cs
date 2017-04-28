using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
	public Canvas proctorGUI;
    private int currentCamera = 0;
    public Camera[] cameras;

	// Use this for initialization
	void Start () {

        foreach (Camera camera in cameras) {
            camera.gameObject.SetActive(false);
        }
        cameras[0].gameObject.SetActive(true);

		proctorGUI = GameObject.FindGameObjectWithTag ("LiveProctorGUI").GetComponent<Canvas>();
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.C)) {
            currentCamera++;
            if (currentCamera < cameras.Length) {
                cameras[currentCamera - 1].gameObject.SetActive(false);
                cameras[currentCamera].gameObject.SetActive(true);
				proctorGUI.worldCamera = cameras[currentCamera];
            } else {
                cameras[currentCamera - 1].gameObject.SetActive(false);
                currentCamera = 0;
                cameras[currentCamera].gameObject.SetActive(true);
				proctorGUI.worldCamera = cameras[currentCamera];
            }
        }
    }
}
