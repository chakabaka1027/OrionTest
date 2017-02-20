using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionTracker : MonoBehaviour {

    Quaternion grabDirection;

    private void Update() {
        if (!FindObjectOfType<DragBehavior>().isRotating) {
            gameObject.transform.position = Camera.main.gameObject.transform.position;
            gameObject.transform.rotation = Camera.main.transform.rotation;
        } 

    }

    public void GrabDirection() {
        grabDirection = Camera.main.transform.rotation;
    }

}
