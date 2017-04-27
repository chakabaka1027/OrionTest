using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    bool hasFinishedLevel = false;    

    public GameObject cursor;
    public GameObject movementCursor;
    public GameObject loadCursor;

    AudioSource audioSource;
    public AudioClip[] move;
    bool hasPlayed = false;
    
    [HideInInspector]
    public GameObject nextNavpoint;
    [HideInInspector]
    public GameObject currentNavpoint;

    GameObject currentNavpointParent;

    public LayerMask moveable;

    //bool canMove = true;

    public bool handOpen = false;
    public bool moveModeActive = false;
    bool isMoving = false;

    private void Start() {
        cursor.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        
        if (moveModeActive) {
            IdentifyNavPoint();

        }
    }

    private void IdentifyNavPoint() {
        Vector3 ray = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit hit;

        if(Physics.Raycast(ray, Camera.main.transform.forward, out hit, Mathf.Infinity, moveable)) {
            if(hit.collider.gameObject.GetComponent<MovementLantern>() != null) {
                if(hit.collider.gameObject.GetComponent<MovementLantern>().isActive && !gameObject.GetComponent<DragBehavior>().rotationModeActive){
                    nextNavpoint = hit.collider.gameObject;
                    movementCursor.SetActive(true);
                }
            }

            //elevator
            else if(hit.collider.gameObject.GetComponent<Elevator>() != null && !hit.collider.gameObject.GetComponent<Elevator>().isStartingElevator && !gameObject.GetComponent<DragBehavior>().rotationModeActive) {
                movementCursor.SetActive(true);

                nextNavpoint = hit.collider.gameObject;
                //canMove = false;

            }

            else {
                if(!isMoving) {
                    nextNavpoint = null;

                }
                movementCursor.SetActive(false);

            }

        } 
    }

    public void MoveToNavPoint() {
        StartCoroutine(Move());
       
    }

    public IEnumerator Move() {

        isMoving = true;

        //player now occupies new lantern
        if(nextNavpoint != null && nextNavpoint.GetComponent<MovementLantern>() != null) {
            nextNavpoint.transform.parent.FindChild("Lantern").GetComponent<MovementLantern>().isCurrentLantern = true;
        }

        if(moveModeActive && nextNavpoint!= null && FindObjectOfType<DragBehavior>().rotationModeActive == false) {

            if (currentNavpoint != null) {
                currentNavpoint.GetComponent<MeshRenderer>().enabled = true;
                currentNavpoint.GetComponent<BoxCollider>().enabled = true;

                //player no longer occupying old lantern
                if(currentNavpoint.GetComponent<MovementLantern>() != null) { 
                    currentNavpoint.transform.parent.FindChild("Lantern").GetComponent<MovementLantern>().isCurrentLantern  = false;
                }

            }

            else if(currentNavpointParent != null && currentNavpoint == null){
                currentNavpointParent.GetComponent<MovementLantern>().isCurrentLantern = false;

            }

            nextNavpoint.GetComponent<MeshRenderer>().enabled = false;
            nextNavpoint.GetComponent<BoxCollider>().enabled = false;

            //trigger any tutorials if they exist
            if(nextNavpoint.GetComponent<TutorialTrigger>() != null) {
                nextNavpoint.GetComponent<TutorialTrigger>().Activate();
            }

            currentNavpoint = nextNavpoint;

            if(nextNavpoint.name == "MovementLanternActual") {
                currentNavpointParent = nextNavpoint.transform.parent.FindChild("Lantern").gameObject;

            }


            movementCursor.SetActive(false);

            //move sound
            if(!hasPlayed) {
                int rand = Random.Range(0, 4);
                audioSource.PlayOneShot(move[rand], .75f);
                hasPlayed = true;
            }
            //interpolate movement
            float percent = 0;
            float time = .6f;
            float speed = 1/time;

            while(percent < 1) {
                percent += Time.deltaTime * speed;
                if (nextNavpoint != null) {
                    gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, nextNavpoint.transform.position, percent);
                }
                yield return null;

            }     
            
               
           
        }
        //is end of level elevator
        if(currentNavpoint != null && currentNavpoint.GetComponent<Elevator>() != null) {
            //yield return new WaitForSeconds(.75f);
            if(nextNavpoint != null && nextNavpoint.GetComponent<Elevator>() != null && !hasFinishedLevel) {
                hasFinishedLevel = true;
                nextNavpoint.GetComponent<Elevator>().InitiateElevator();
            }
        }

        isMoving = false;
        hasPlayed = false;
    }

    //called when hand is open, playing cursor animation before allowing movement
    public void ActivateCursor() {

        if(!gameObject.GetComponent<DragBehavior>().rotationModeActive ) {
            loadCursor.SetActive(true);
            loadCursor.GetComponent<Animator>().Play("InitiateCursor");
        

            handOpen = true;        
            EngageMovementMode();

        }
    }

    public void EngageMovementMode() {
        if(!FindObjectOfType<DragBehavior>().isRotating) {
            if(!gameObject.GetComponent<DragBehavior>().rotationModeActive) {
                cursor.SetActive(true);
            } 
            moveModeActive = true;
        }
    }


    public void DeactivateCursor() {
        StartCoroutine(DisengageMovementMode());
    }

    IEnumerator DisengageMovementMode() {
        if(!FindObjectOfType<DragBehavior>().rotationModeActive) {    
            loadCursor.SetActive(false);
            StopCoroutine("LoadMovementMode");
            cursor.SetActive(false);
            handOpen = false;
            yield return new WaitForSeconds(0.3f);
            moveModeActive = false;
        }
    }

}
