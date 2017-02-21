using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionTracker : MonoBehaviour {

    Quaternion grabDirection;

    private void Update() {
        if (!FindObjectOfType<DragBehavior>().isRotating) {
            gameObject.transform.position = Camera.main.gameObject.transform.position;
            gameObject.transform.rotation = Camera.main.transform.rotation; //try comparing to vector3.zero
        } 

    }

    public void GrabDirection() {
        grabDirection = Camera.main.transform.rotation;
    }

}
