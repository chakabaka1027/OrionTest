using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;
using Hover.InterfaceModules.Cast;

public class DragBehavior : MonoBehaviour {

    public GameObject cameraReference;
    public GameObject cameraReferenceUpandDown;

    [Header("Palm Info")]
    public GameObject rightPalm;
    public LayerMask rotatable;    

    [Header("Cursors")]
    public GameObject openHandCursor;
    public GameObject closedHandCursor;

    
    [Header("Rotation UI")]
    public GameObject rotationPanel;
    public GameObject rotatorCompass;

    public GameObject arrow;

    public GameObject rightArrowLocation;
    public GameObject leftArrowLocation;
    public GameObject upArrowLocation;
    public GameObject downArrowLocation;

    GameObject myCompass;
    GameObject myLeftArrow;
    GameObject myRightArrow;
    GameObject myUpArrow;
    GameObject myDownArrow;

    Vector3 rightArrowScale;
    Vector3 leftArrowScale;
    Vector3 upArrowScale;
    Vector3 downArrowScale;


    float sensitivity;
    Vector3 palmPosReference;
    Vector3 palmOffset;
    Vector3 rotation;

    [HideInInspector]
    public bool rotationModeActive = false;
    [HideInInspector]
    public bool isRotating;
    [HideInInspector]
    public GameObject mirror;

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
                rotation.y = (palmOffset.x) * sensitivity;
                mirror.transform.Rotate(0, rotation.y, 0);
                
                //rotate ui compass
                if(myCompass != null) {
                    myCompass.transform.Rotate(0, 0, -rotation.y);
                }
                
                //enlargen ui arrow
                if(myRightArrow != null && myLeftArrow != null) {
   
                //make right arrow bigger, left arrow smaller
                    if(rightPalm.transform.localPosition.x > palmPosReference.x) {
                        myRightArrow.transform.localScale += new Vector3(-palmOffset.x, -palmOffset.x, -palmOffset.x) * 1f;
                        myLeftArrow.transform.localScale += new Vector3(palmOffset.x, palmOffset.x, palmOffset.x) * 1f;

                    } 
                //make left arrow bigger, right arrow smaller 
                    if(rightPalm.transform.localPosition.x < palmPosReference.x) {
                        myRightArrow.transform.localScale -= new Vector3(palmOffset.x, palmOffset.x, palmOffset.x) * 1f;
                        myLeftArrow.transform.localScale -= new Vector3(-palmOffset.x, -palmOffset.x, -palmOffset.x) * 1f;

                    }  
                    
                    //lock scales of arrows to a certain size
                    if(myRightArrow.transform.localScale.x < rightArrowScale.x) {
                        myRightArrow.transform.localScale = rightArrowScale;
                    }   
                    if(myLeftArrow.transform.localScale.x < leftArrowScale.x) {
                        myLeftArrow.transform.localScale = leftArrowScale;
                    }             
                }
                

            }

            if (mirror.gameObject.name == "MirrorY"){
            //apply rotation up and down
                rotation.x = (palmOffset.y) * sensitivity;
                mirror.transform.Rotate(rotation.x, 0, 0);

                //rotate ui compass
                if(myCompass != null) {
                    myCompass.transform.Rotate(0, 0, -rotation.x);
                }

                //enlargen ui arrow
                if(myUpArrow != null && myDownArrow != null) {
   
                //make up arrow bigger, down arrow smaller
                    if(rightPalm.transform.localPosition.y > palmPosReference.y) {
                        myUpArrow.transform.localScale += new Vector3(-palmOffset.y, -palmOffset.y, -palmOffset.y) * 1.25f;
                        myDownArrow.transform.localScale += new Vector3(palmOffset.y, palmOffset.y, palmOffset.y) * 1.25f;

                    } 
                //make down arrow bigger, up arrow smaller 
                    if(rightPalm.transform.localPosition.y < palmPosReference.y) {
                        myUpArrow.transform.localScale -= new Vector3(palmOffset.y, palmOffset.y, palmOffset.y) * 1.25f;
                        myDownArrow.transform.localScale -= new Vector3(-palmOffset.y, -palmOffset.y, -palmOffset.y) * 1.25f;

                    }  
                    
                    //lock scales of arrows to a certain size
                    if(myUpArrow.transform.localScale.x < upArrowScale.x) {
                        myUpArrow.transform.localScale = upArrowScale;
                    }   
                    if(myDownArrow.transform.localScale.x < downArrowScale.x) {
                        myDownArrow.transform.localScale = downArrowScale;
                    }             
                }

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
            //deactivate wrist UI
            FindObjectOfType<HovercastInterface>().IsOpen = false;

            //animate and type text for HUD
            rotationPanel.GetComponent<Animator>().Play("Opened");
            mirror.GetComponent<Animator>().Play("IncreaseEmission");


            //if(!rotationModeActive) {
                if(mirror.name == "MirrorY") {
                    if(!rotationModeActive) {
                        rotationPanel.transform.GetComponentInChildren<Text>().text = "";

                        myCompass = Instantiate(rotatorCompass, mirror.transform.position, Quaternion.Euler(0, mirror.transform.localRotation.y + 45, 0)) as GameObject;
                        StartCoroutine(rotationPanel.transform.GetComponentInChildren<Typing>().TypeIn("Vertical Rotation "));
                    }

                    Vector3 mirrorVector = cameraReference.transform.forward;
                   
                    Vector3 left = mirrorVector - cameraReference.transform.right;
                    Vector3 up = Vector3.Cross(mirrorVector.normalized, left.normalized);
                    

                    myUpArrow = Instantiate(arrow, rightPalm.transform.position + up * 0.01f ,  Quaternion.Euler(-90, 0, 0)) as GameObject;        
                    myDownArrow = Instantiate(arrow, rightPalm.transform.position - up * 0.001f ,  Quaternion.Euler(90, 0, 0)) as GameObject;
                  

                    upArrowScale = myUpArrow.transform.localScale;
                    downArrowScale = myDownArrow.transform.localScale;

                } else if (mirror.name == "MirrorX") { 
                    if(!rotationModeActive) {
                        rotationPanel.transform.GetComponentInChildren<Text>().text = "";

                        myCompass = Instantiate(rotatorCompass, mirror.transform.position, Quaternion.Euler(90, 0 , 0)) as GameObject;
                        StartCoroutine(rotationPanel.transform.GetComponentInChildren<Typing>().TypeIn("Horizontal Rotation "));

                    }

                    //a way of using cross product to find the right and left vectors from the player's facing direction.
                    Vector3 mirrorVector = cameraReference.transform.forward;
                    Vector3 up = new Vector3(0, 1, 0);
                    Vector3 left = Vector3.Cross(mirrorVector.normalized, up.normalized);
                    Vector3 right = -left;
                    

                    myRightArrow = Instantiate(arrow, rightPalm.transform.position + right * 0.01f , Quaternion.Euler(0, cameraReference.transform.eulerAngles.y + 90, 0)) as GameObject;
                    myLeftArrow = Instantiate(arrow, rightPalm.transform.position + left * 0.01f , Quaternion.Euler(0, cameraReference.transform.eulerAngles.y - 90, 0)) as GameObject;
                    
                    //myRightArrow.GetComponent<Animator>().Play("ArrowOpen");
                    //myLeftArrow.GetComponent<Animator>().Play("ArrowOpen");

                    rightArrowScale = myRightArrow.transform.localScale;
                    leftArrowScale = myLeftArrow.transform.localScale;
                }
            //}

            rotationModeActive = true;
            isRotating = true;
            palmPosReference = rightPalm.transform.localPosition;
            FindObjectOfType<DirectionTracker>().GrabDirection();

        }
    }

    public void StopRotatingLeftRight() {
        isRotating = false;
        palmPosReference = Vector3.zero;

        Destroy(myLeftArrow, .5f);
        Destroy(myRightArrow, .5f);
        
        Destroy(myUpArrow, .5f);
        Destroy(myDownArrow, .5f);

        if(myUpArrow != null) {
            myUpArrow.transform.GetChild(0).GetComponent<Animator>().Play("ArrowClosed");
        }

        if(myDownArrow != null) {
            myDownArrow.transform.GetChild(0).GetComponent<Animator>().Play("ArrowClosed");

        }

        if(myLeftArrow != null) {
            myLeftArrow.transform.GetChild(0).GetComponent<Animator>().Play("ArrowClosed");
        }

        if(myRightArrow != null) {
            myRightArrow.transform.GetChild(0).GetComponent<Animator>().Play("ArrowClosed");

        }
    }

    public void FinishedRotating() {
       StartCoroutine(FinishedRotatingCoroutine());
    }

    IEnumerator FinishedRotatingCoroutine() {
    rotationPanel.GetComponent<Animator>().Play("Closed");
       
        yield return new WaitForSeconds(.5f);
        rotationPanel.transform.GetComponentInChildren<Text>().text = "";

        Destroy(myCompass);
        if(mirror!=null) {
            mirror.GetComponent<Animator>().Play("DecreaseEmission");

        }


        rotationModeActive = false;
    }


}
