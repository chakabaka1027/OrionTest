using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class DragBehavior : MonoBehaviour {

    public GameObject rightPalm;
    public GameObject openHandCursor;
    public GameObject closedHandCursor;

    public LayerMask rotatable;

    GameObject mirror;

    private float sensitivity;
     private Vector3 palmPosReference;
     private Vector3 palmOffset;
     private Vector3 rotation;
     public bool isRotating;

	// Use this for initialization
	void Start () {
		sensitivity = 150;
        rotation = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {

        IdentifyRotationObj();
	    if(isRotating && mirror != null) {
            closedHandCursor.SetActive(true);

            //offset
            palmOffset = -( rightPalm.transform.localPosition - palmPosReference);

            

            //apply rotation
            rotation.y = (palmOffset.x /*+ palmOffset.y*/) * sensitivity;

            //rotate
            mirror.transform.Rotate(rotation);

            //store palm
            palmPosReference = rightPalm.transform.localPosition;


        } else {
            closedHandCursor.SetActive(false);
        }


	}

    private void IdentifyRotationObj() {
        Vector3 ray = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit hit;

        if(Physics.Raycast(ray, Camera.main.transform.forward, out hit, Mathf.Infinity, rotatable)) {
            mirror = hit.collider.gameObject;
            openHandCursor.SetActive(true);

        } else {
            openHandCursor.SetActive(false);
            if (!isRotating) {
                mirror = null;
            }
        }
    }



    public void RotateMirrorLeftRight() {

        if(openHandCursor.activeInHierarchy) {
            isRotating = true;
            
            palmPosReference = rightPalm.transform.localPosition;
        }
       
    }

    public void StopRotatingLeftRight() {
        isRotating = false;
    }

}
