using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLaser : MonoBehaviour {

    public LayerMask lasers;
    public GameObject laserOnUI;
    public GameObject laserOffUI;
    public GameObject defaultCursor;

    GameObject currentLaser;

	
	// Update is called once per frame
	void Update () {
        Vector3 ray = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit hit;

        if(Physics.Raycast(ray, Camera.main.transform.forward, out hit, Mathf.Infinity, lasers)){
            defaultCursor.SetActive(false);

            if(hit.collider.gameObject.transform.FindChild("Laser").GetComponentInChildren<RaycastReflection>().isActive == 0) {
                laserOnUI.SetActive(true);
                laserOffUI.SetActive(false);
            } else if(hit.collider.gameObject.transform.FindChild("Laser").GetComponentInChildren<RaycastReflection>().isActive == 1) {
               laserOffUI.SetActive(true);
               laserOnUI.SetActive(false);
            }
            currentLaser = hit.collider.gameObject;

        } else {
            currentLaser = null;
            laserOffUI.SetActive(false);
            laserOnUI.SetActive(false);
            defaultCursor.SetActive(true);

        }
    }

    public void ToggleLaser() {
        if(currentLaser != null) {
            currentLaser.gameObject.transform.FindChild("Laser").GetComponentInChildren<RaycastReflection>().isActive = 1 - currentLaser.gameObject.transform.FindChild("Laser").GetComponentInChildren<RaycastReflection>().isActive;
            StartCoroutine(currentLaser.gameObject.transform.FindChild("Laser").GetComponentInChildren<RaycastReflection>().LaserSwitch());
        }
    }
}
