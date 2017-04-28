using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineOutline : MonoBehaviour {
    private MeshRenderer meshRenderer;
    private Material outlineMaterial;
    private const string magazineName = "Magazine";
    private const float outlineThickness = 0.5f;
    private const string magazineOutline = "OutlineMagazine";
    private GameObject outline;

    // Use this for initialization
    void Start () {
        outline = transform.Find(magazineOutline).gameObject;
        meshRenderer = outline.GetComponent<MeshRenderer>();
        outlineMaterial = meshRenderer.materials[1];
        if (IHaveAMagazine()) {
            TurnOffOutline();
        } else {
            TurnOnOutline();
            outlineMaterial.SetColor("_OutlineColor", Color.red);
        }
    }

    public void OnTriggerEnter(Collider other) {
        if (other.transform.name.Equals(magazineName)) {
            if (!other.transform.GetComponent<Magazine>().isSnapped && !IHaveAMagazine()) {
                outlineMaterial.SetColor("_OutlineColor", Color.green);
            }
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.transform.name.Equals(magazineName)) {
            if (!IHaveAMagazine()) {
                TurnOnOutline();
                outlineMaterial.SetColor("_OutlineColor", Color.red);
            }
        }
    }

    private bool IHaveAMagazine() {
        if (transform.Find(magazineName)) {
            return true;
        } else {
            return false;
        }
    }

    public void TurnOffOutline() {
        outlineMaterial.SetFloat("_Thickness", 0.0f);
    }

    private void TurnOnOutline() {
        outlineMaterial.SetFloat("_Thickness", outlineThickness);
    }
}
