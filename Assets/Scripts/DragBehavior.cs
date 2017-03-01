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
            openHandCursor.SetActive(false);

            //offset
            palmOffset = -( rightPalm.transform.localPosition - palmPosReference);


            //apply rotation left and right
            if (mirror.gameObject.name == "MirrorX"){
                rotation.y = (palmOffset.x /*+ palmOffset.y*/) * sensitivity;
                mirror.transform.Rotate(0, rotation.y, 0);

            }

            if (mirror.gameObject.name == "MirrorY"){
            //apply rotation up and down
                rotation.x = (palmOffset.y /*+ palmOffset.y*/) * sensitivity;
                mirror.transform.Rotate(-rotation.x, 0, 0);

            }

            //rotate
            
            //mirror.transform.Rotate(rotation);

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
            FindObjectOfType<DirectionTracker>().GrabDirection();

        }
       
    }

    public void StopRotatingLeftRight() {
        isRotating = false;
        palmPosReference = Vector3.zero;
    }

}
