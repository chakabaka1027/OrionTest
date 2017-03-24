using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public GameObject cursor;
    public GameObject movementCursor;
    public GameObject loadCursor;
    GameObject nextNavpoint;
    GameObject currentNavpoint;

    public LayerMask moveable;

    public bool handOpen = false;
    public bool moveModeActive = false;
    bool isMoving = false;

    private void Start() {
        cursor.SetActive(false);
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
                if(hit.collider.gameObject.GetComponent<MovementLantern>().isActive){
                    nextNavpoint = hit.collider.gameObject;
                    movementCursor.SetActive(true);
                }
            }

            else {
                if(!isMoving) {
                    nextNavpoint = null;

                }
                movementCursor.SetActive(false);

            }

            //uncomment if you want to move instantly when looking at desired navpoint
            //MoveToNavPoint();

        } 
    }

    public void MoveToNavPoint() {
        StartCoroutine(Move());
       
    }

    public IEnumerator Move() {

        isMoving = true;

        if(moveModeActive && nextNavpoint!= null /*&& isMoving*/ && FindObjectOfType<DragBehavior>().rotationModeActive == false) {

            if (currentNavpoint != null) {
                currentNavpoint.SetActive(true);
            }

            nextNavpoint.SetActive(false);
            currentNavpoint = nextNavpoint;
            movementCursor.SetActive(false);

            //test stuff below
            float percent = 0;
            float time = 0.3f;
            float speed = 1/time;

            while(percent < 1) {
                percent += Time.deltaTime * speed;
                if (nextNavpoint != null) {
                    gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, nextNavpoint.transform.position, percent);
                }
                yield return null;
            }

            

            //gameObject.transform.position = nextNavpoint.transform.position;
            
           
        }
        isMoving = false;

    }

    //called when hand is open, playing cursor animation before allowing movement
    public void ActivateCursor() {
        loadCursor.SetActive(true);
        loadCursor.GetComponent<Animator>().Play("InitiateCursor");
        handOpen = true;

        EngageMovementMode();

    }

    public void EngageMovementMode() {
        if(FindObjectOfType<DragBehavior>().isRotating == false) {
            cursor.SetActive(true);
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
