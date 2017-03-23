using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;

public class DragBehavior : MonoBehaviour {

    public GameObject rightPalm;
    public GameObject openHandCursor;
    public GameObject closedHandCursor;

    public LayerMask rotatable;

    public GameObject mirror;
    public GameObject rotationPanel;
    public GameObject rotatorCompass;

    GameObject myCompass;

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
                
                //rotate ui compass
                myCompass.transform.Rotate(0, 0, -rotation.y);

            }

            if (mirror.gameObject.name == "MirrorY"){
            //apply rotation up and down
                rotation.x = (palmOffset.y /*+ palmOffset.y*/) * sensitivity;
                mirror.transform.Rotate(-rotation.x, 0, 0);

                //rotate ui compass
                myCompass.transform.Rotate(0, 0, -rotation.x);

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
            if(hit.collider.gameObject.name == "MirrorX" || hit.collider.gameObject.name == "MirrorY") {
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
            //animate and type text for HUD
            rotationPanel.GetComponent<Animator>().Play("Opened");

            if(!rotationModeActive) {
                rotationPanel.transform.GetComponentInChildren<Text>().text = "";
                if(mirror.name == "MirrorY") {
                    StartCoroutine(rotationPanel.transform.GetComponentInChildren<Typing>().TypeIn("Vertical Rotation "));
                    myCompass = Instantiate(rotatorCompass, mirror.transform.position, Quaternion.Euler(0, mirror.transform.localRotation.y + 45, 0)) as GameObject;
                    //myCompass.transform.parent = mirror.transform;
                } else if (mirror.name == "MirrorX") {
                    StartCoroutine(rotationPanel.transform.GetComponentInChildren<Typing>().TypeIn("Horizontal Rotation "));
                    myCompass = Instantiate(rotatorCompass, mirror.transform.position, Quaternion.Euler(90, 0 , 0)) as GameObject;
                    //myCompass.transform.parent = mirror.transform;
                }
            }

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
       StartCoroutine(FinishedRotatingCoroutine());
    }

    IEnumerator FinishedRotatingCoroutine() {
    rotationPanel.GetComponent<Animator>().Play("Closed");
       
        yield return new WaitForSeconds(.5f);
        rotationPanel.transform.GetComponentInChildren<Text>().text = "";

        Destroy(myCompass);

        rotationModeActive = false;
    }


}
