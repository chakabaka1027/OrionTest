using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    int toggle = 0;
    bool isRotating = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isRotating) {
            gameObject.transform.Rotate(Vector3.forward);
        } else {
            gameObject.transform.rotation = gameObject.transform.rotation;

        }

	}

    public void RotationToggle() {


        toggle = 1 - toggle;

        if (toggle == 0) {
            isRotating = false;
        } else {
            isRotating = true;
        }

    }




}
