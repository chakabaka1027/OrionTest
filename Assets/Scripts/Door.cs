using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public Transform closedPos;
    public Transform openedPos;

    Vector3 closedPosition;
    Vector3 openedPosition;

    int toggle = 1;

	// Use this for initialization
	void Start () {
	    closedPosition = closedPos.position;
        openedPosition = openedPos.position;

	}
	
	

    
    public void ToggleDoor() {
        toggle = 1 - toggle;
        if (toggle == 1) {
            StartCoroutine("OpenDoor");
        } else {
            StartCoroutine("CloseDoor");
        }
    }
    
    
    public IEnumerator OpenDoor() {
        
        StopCoroutine("CloseDoor");
        float percent = 0;
        float time = 1;
        float speed = 1 / time;
        Vector3 currentPos = transform.position;

        while(percent < 1) {
            percent += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(currentPos, openedPosition, percent);
            yield return null;
        }

    }

    public IEnumerator CloseDoor() {
        StopCoroutine("OpenDoor");

        float percent = 0;
        float time = 1;
        float speed = 1 / time;
        Vector3 currentPos = transform.position;

        while(percent < 1) {
            percent += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(currentPos, closedPosition, percent);
            yield return null;
        }

    }
}
