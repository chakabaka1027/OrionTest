using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLantern : MonoBehaviour {

    public int activationSwitch = 0;
    public bool isActive = false;

	
	// Update is called once per frame
	void Update () {
		
	}

     public void MovementLanternSwitch() {
        activationSwitch = 1 - activationSwitch;

        if(activationSwitch == 1) {
            gameObject.transform.GetChild(0).GetComponent<Light>().enabled = true;
            isActive = true;
        } else if (activationSwitch == 0) {
            gameObject.transform.GetChild(0).GetComponent<Light>().enabled = false;
            isActive = false;
        }
    }
}
