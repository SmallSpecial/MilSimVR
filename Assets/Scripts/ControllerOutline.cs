using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerOutline : MonoBehaviour {

    private MeshRenderer meshRenderer;
    private Material outlineMaterial;
    private const float outlineThickness = 0.5f;

    private const string controllerRight = "Controller (right)";
    private const string controllerLeft = "Controller (left)";
    private const string controllerBody = "Head";

    void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
        outlineMaterial = meshRenderer.materials[0];
    }

    private void OnTriggerEnter(Collider other) {
        print(other.transform.name);
        if (other.transform.name == controllerRight || other.transform.name == controllerLeft  || other.transform.name == controllerBody) {
            outlineMaterial.SetFloat("_Thickness", 0.0f);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.transform.name == controllerRight || other.transform.name == controllerLeft || other.transform.name == controllerBody) {
            outlineMaterial.SetFloat("_Thickness", outlineThickness);
        }
    }
}
