using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour {

    public const float magazineSize = 30;

    public bool isSnapped = false;

    public float bulletsInMag;

    void Start() {
        bulletsInMag = magazineSize;
    }
}
