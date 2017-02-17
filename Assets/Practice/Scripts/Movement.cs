﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public GameObject cursor;
    public GameObject movementCursor;
    GameObject nextNavpoint;
    GameObject currentNavpoint;

    public LayerMask moveable;

    bool moveModeActive = false;
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
            nextNavpoint = hit.collider.gameObject;
            movementCursor.SetActive(true);

            //uncomment if you want to move instantly when looking at desired navpoint
            //MoveToNavPoint();

        } else {
            if(!isMoving) {
                nextNavpoint = null;

            }
            movementCursor.SetActive(false);

        }
    }

    public void MoveToNavPoint() {
        StartCoroutine(Move());
       
    }

    public IEnumerator Move() {
        isMoving = true;

        if(moveModeActive && nextNavpoint!= null && FindObjectOfType<DragBehavior>().isRotating == false && isMoving) {
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

    public void EngageMovementMode() {
        cursor.SetActive(true);
        moveModeActive = true;
    }

    public void DisengageMovementMode() {
        cursor.SetActive(false);
    }

    public IEnumerator RemoveCursor() {
        yield return new WaitForSeconds(0.01f);
        moveModeActive = false;
        
        
        
    }


  
}
