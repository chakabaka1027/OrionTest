using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class HandLocationTracker : MonoBehaviour {

    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject rightHandClone;

    public bool isLeft;

	
	void Update () {
		if (isLeft) {
            gameObject.transform.position = leftHand.transform.position;
        } else if (rightHand.activeSelf) {
            gameObject.transform.position = rightHand.transform.position;
        } 
        
        if(GameObject.Find("RigidRoundHand_R(Clone)") != null) {
            rightHandClone = GameObject.Find("RigidRoundHand_R(Clone)");
        }

        if(rightHandClone != null && rightHandClone.activeSelf) {
            gameObject.transform.position = rightHandClone.transform.FindChild("palm").transform.position;

        }

        
        //GameObject.Find("R FindObjectOfType<HandModel>().GetPalmPosition();
	}
}
