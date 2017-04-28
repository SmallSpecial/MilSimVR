using System.Collections;
using System;
using UnityEngine;
using VRTK;

public class Shoot : VRTK_InteractableObject {
    private VRTK_ControllerActions controllerActions;
    private VRTK_ControllerEvents controllerEvents;
    private GameObject endOfBarrel;
    public AudioSource audioSource;
    public AudioClip gunshotClip;
	public AudioClip hitMetal1;
	//public AudioClip hitMetal2;
	public GameObject muzzleFlash;
	private GameObject flash;
	private GameManager GM;

    public float hapticStrength = 1.0f;
    public float hapticDuration = 0.1f;
    public float hapticInterval = 0.1f;

	public GameObject decalHitWall;
	public float floatInFrontOfWall = 0.00001f;


	private float reloadTime = 2f;
	private float timeLeftInReload;

	public VRTK_ControllerEvents VRTK_CE;

	private GameObject mainGrabbingHand;
	private string magazinePath = "MagazineSnapPoint/Magazine";
	private Magazine magazineInGun;

    void Start() {
		timeLeftInReload = 0;
        endOfBarrel = GameObject.Find("EndOfBarrel");
		GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public override void StartUsing(GameObject usingObject) {
        base.StartUsing(usingObject);
		if (HasBulletsInMag()) {
			FireBullet();
		}
    }




    public override void Grabbed(GameObject currentGrabbingObject) {
        base.Grabbed(currentGrabbingObject);
        //When grabbed it will replace controllerActions with the controller that just grabbed it.
        //Need to keep up with all controllers currently grabbing so we can vibrate all or just the one needed.
		if (!mainGrabbingHand) {
			controllerActions = currentGrabbingObject.GetComponent<VRTK_ControllerActions>();
			controllerEvents = currentGrabbingObject.GetComponent<VRTK_ControllerEvents>(); //Used for button presses
			mainGrabbingHand = currentGrabbingObject;
		}
    }

    public override void Ungrabbed(GameObject previousGrabbingObject) {
        base.Ungrabbed(previousGrabbingObject);
		if (mainGrabbingHand == previousGrabbingObject) {
			mainGrabbingHand = null;
			controllerEvents = null;
			controllerActions = null;
		}
    }

    public void FireBullet() {
        if (flash == null) {
			flash = GameObject.Instantiate(muzzleFlash, endOfBarrel.transform.position, endOfBarrel.transform.rotation);
        }
        else {
            flash.SetActive(true);
        }
        
		Magazine mag = transform.Find(magazinePath).GetComponent<Magazine>();
		mag.bulletsInMag--;
		GM.shotsFired++;
        audioSource.PlayOneShot(gunshotClip, 1);
		if (controllerActions) {
			controllerActions.TriggerHapticPulse(hapticStrength, hapticDuration, hapticInterval);
		}

        RaycastHit hit;
        if (Physics.Raycast(endOfBarrel.transform.position, endOfBarrel.transform.forward, out hit)) {
            if (hit.transform.tag.Equals("Enemy")) {
				ShootEnemy(hit.transform.gameObject);
            } else if (hit.transform.tag.Equals("Civ")) {
				ShootCiv(hit.transform.gameObject);
            } else {
                print("I hit: " + hit.transform.name);
            }


			if (hit.transform.gameObject.GetComponent<ShotSound>()) {
				hit.transform.gameObject.GetComponent<ShotSound>().Play();
			}
            GameObject decal = Instantiate(decalHitWall, hit.point + (hit.normal * floatInFrontOfWall), Quaternion.LookRotation(hit.normal));
			decal.transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(0, 360)));
			decal.transform.localScale = decal.transform.localScale * UnityEngine.Random.Range(.5f, 1);
			decal.transform.SetParent(hit.transform, true);
        }
    }

    public Transform GetMagazine() {
        return transform.Find(magazinePath);
    }

	private bool HasBulletsInMag() {
        Transform magazine = GetMagazine();
        if (magazine != null) {
            Magazine mag = magazine.GetComponent<Magazine>();
            if (mag.bulletsInMag > 0) {
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}
	}

	private void ShootEnemy(GameObject enemy) {
		print("I hit: " + enemy.transform.name + " who is an enemy");
        GetShot getShot = enemy.GetComponent<GetShot>();
        if (!getShot.haveIBeenShot) {
            GM.numberOfEnemiesShot++;
        }
        getShot.Shot();
	}

	private void ShootCiv(GameObject civ) {
		print("I hit: " + civ.transform.name + " who is a civ");
        GetShot getShot = civ.GetComponent<GetShot>();
        if (!getShot.haveIBeenShot) {
            GM.numberOfCiviliansShot++;
        }
        getShot.Shot();
	}

}
