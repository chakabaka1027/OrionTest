using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public GameObject cursor;
    GameObject nextNavpoint;
    GameObject currentNavpoint;

    public LayerMask moveable;

    bool canMove = false;

    private void Start() {
        cursor.SetActive(false);
    }

    private void Update() {
        //if(Input.GetKeyDown(KeyCode.Space)) {
        //    MoveToNavPoint();
        //}
        if (canMove) {
            IdentifyNavPoint();

        }
    }

    private void IdentifyNavPoint() {
        Vector3 ray = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit hit;

        if(Physics.Raycast(ray, Camera.main.transform.forward, out hit, Mathf.Infinity, moveable) && canMove) {
            nextNavpoint = hit.collider.gameObject;
            
            //uncomment if you want to move instantly when looking at desired navpoint
            //MoveToNavPoint();

        } else {
            nextNavpoint = null;
        }
    }

    public void MoveToNavPoint() {
        if(canMove && nextNavpoint!= null) {
            if (currentNavpoint != null) {
                currentNavpoint.SetActive(true);
            }

            gameObject.transform.position = nextNavpoint.transform.position;
            nextNavpoint.SetActive(false);
            currentNavpoint = nextNavpoint;
            cursor.SetActive(false);
        }
    }

    public void EngageMovementMode() {
        if(!canMove) {
            cursor.SetActive(true);
            canMove = true;
        }
    }

    public void DisengageMovementMode() {
        if (canMove) {
            cursor.SetActive(false);
            canMove = false;
        }
        
        
        
    }


  
}
