using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class DragBehavior : MonoBehaviour {

    public GameObject rightPalm;
    public GameObject openHandCursor;
    public GameObject closedHandCursor;

    public LayerMask rotatable;

    public GameObject mirror;

    private float sensitivity;
     private Vector3 palmPosReference;
     private Vector3 palmOffset;
     private Vector3 rotation;
     public bool rotationModeActive = false;
     public bool isRotating;

	// Use this for initialization
	void Start () {
		sensitivity = 65;
        rotation = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        if(!FindObjectOfType<Movement>().moveModeActive) {
            mirror = null;
        }
        
        else if(FindObjectOfType<Movement>().moveModeActive) {
            IdentifyRotationObj();
        }

	    if(rotationModeActive && mirror != null && isRotating) {
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
                mirror.transform.Rotate(rotation.x, 0, 0);

            }

            //store palm
            palmPosReference = rightPalm.transform.localPosition;


        } else {
            closedHandCursor.SetActive(false);
            isRotating = false;
        }


	}

    private void IdentifyRotationObj() {
        Vector3 ray = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit hit;

        if(Physics.Raycast(ray, Camera.main.transform.forward, out hit, Mathf.Infinity, rotatable) && FindObjectOfType<Movement>().moveModeActive) {
            if(hit.collider.gameObject.name == "MirrorX" || hit.collider.gameObject.name == "Mirror Y") {
                mirror = hit.collider.gameObject;
                
                if(FindObjectOfType<Movement>().moveModeActive && mirror.name == "MirrorX" || mirror.name == "MirrorY") {
                    openHandCursor.SetActive(true);

                }
            }
            
            else {
                openHandCursor.SetActive(false);
                if (!rotationModeActive) {
                    mirror = null;
                }
            }

        } 
    }



    public void RotateMirrorLeftRight() {
        if(mirror != null) {
            rotationModeActive = true;
            isRotating = true;
            palmPosReference = rightPalm.transform.localPosition;
            FindObjectOfType<DirectionTracker>().GrabDirection();
        }
    }

    public void StopRotatingLeftRight() {
        isRotating = false;
        palmPosReference = Vector3.zero;
    }

    public void FinishedRotating() {
        rotationModeActive = false;
        Debug.Log("finished!");
    }

}
