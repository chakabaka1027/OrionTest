using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLocationTracker : MonoBehaviour {

    public GameObject leftHand;
    public GameObject rightHand;

    public bool isLeft;

	
	void Update () {
		if (isLeft) {
            gameObject.transform.position = leftHand.transform.position;
        } else {
            gameObject.transform.position = rightHand.transform.position;
        }
	}
}
