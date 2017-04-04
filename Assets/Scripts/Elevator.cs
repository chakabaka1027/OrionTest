using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    public bool isOccupied = false;
    public bool isStartingElevator = false;

    public GameObject target;

    public GameObject player;

    void Start() { 

        if(isStartingElevator) {
            StartCoroutine(Activate());
        }

        target.GetComponent<MeshRenderer>().enabled = false;
    }

    void Update() {
        
        if(isOccupied) {
            player.transform.position = gameObject.transform.position;

        }

    }	

    public void InitiateElevator() {
        StartCoroutine(Activate());

    }

    public IEnumerator Activate() {

        Vector3 startLocation = gameObject.transform.position;
        Vector3 endLocation = target.transform.position;

        isOccupied = true;

        float percent = 0;
        float time = 5;
        float speed = 1 / time;

        while (percent < 1) {
            percent += Time.deltaTime * speed;
            gameObject.transform.position = Vector3.Lerp(startLocation, endLocation, percent); 

            yield return null;
        }

       isOccupied = false;


    }

}
