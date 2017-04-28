using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GrabMagazine : VRTK_InteractableObject {
    public AudioClip reloadClip, unloadClip;
    private MeshRenderer meshRenderer;
    private Material outlineMaterial;

    private string magazineSnapPoint = "MagazineSnapPoint";
    private string magazineName = "Magazine";
    private Transform snapPoint;
	private Shoot gun;

    private uint numberOfSnapCollisions;

    private float outlineThickness = 0.5f;

    private bool ableToBeSnapped;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        numberOfSnapCollisions = 1;
        meshRenderer = GetComponent<MeshRenderer>();
        outlineMaterial = meshRenderer.materials[1];
        outlineMaterial.SetFloat("_Thickness", 0.0f);
        outlineMaterial.SetColor("_OutlineColor", Color.red);
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
		gun = GameObject.Find("M4A1").GetComponent<Shoot>();
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();
        
	}

    public void OnTriggerEnter(Collider other) {
        whenStartCollision(other.gameObject);
    }

    public void OnTriggerExit(Collider other) {
        whenStopCollision(other.gameObject);
    }

    public void OnCollisionEnter(Collision collision) {
        whenStartCollision(collision.gameObject);
    }

    public void OnCollisionExit(Collision collision) {
        whenStopCollision(collision.gameObject);
    }

    public override void Grabbed(GameObject currentGrabbingObject) {
        base.Grabbed(currentGrabbingObject);
        GetComponent<Magazine>().isSnapped = false;
        outlineMaterial.SetFloat("_Thickness", outlineThickness);
        GetComponent<BoxCollider>().isTrigger = true;
        if (ableToBeSnapped) {
            AudioSource.PlayClipAtPoint(unloadClip, snapPoint.transform.position);
        }
    }

    public override void Ungrabbed(GameObject previousGrabbingObject) {
        base.Ungrabbed(previousGrabbingObject);
        outlineMaterial.SetFloat("_Thickness", 0.0f);
        if (ableToBeSnapped) {
            transform.parent = snapPoint;
            snapPoint.GetComponent<MagazineOutline>().TurnOffOutline();
            transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, transform.rotation.w);
            rb.isKinematic = true;
            GetComponent<BoxCollider>().isTrigger = true;
            AudioSource.PlayClipAtPoint(reloadClip, snapPoint.transform.position);
            GetComponent<Magazine>().isSnapped = true;
        } else {
            transform.parent = null;
            rb.isKinematic = false;
            GetComponent<BoxCollider>().isTrigger = false;
        }
    }

    private void whenStartCollision(GameObject other) {
        if ((other.name.Equals(magazineSnapPoint) && !other.transform.Find(magazineName))) {
            numberOfSnapCollisions++;
            outlineMaterial.SetColor("_OutlineColor", Color.green);
            ableToBeSnapped = true;
            snapPoint = other.transform;
        }
    }

    private void whenStopCollision(GameObject other) {
        if (other.name.Equals(magazineSnapPoint) && !other.transform.Find(magazineName)) {
            if (numberOfSnapCollisions != 0) {
                numberOfSnapCollisions--;
            }
            print(numberOfSnapCollisions);
            if (numberOfSnapCollisions <= 0) {
                outlineMaterial.SetColor("_OutlineColor", Color.red);
                ableToBeSnapped = false;
            }
        }
    }
}
