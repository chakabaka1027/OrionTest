using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {

    bool sensor1Active = false;
    public GameObject cube;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Activate() {
        if (gameObject.tag == "Sensor1" && !sensor1Active) {
            sensor1Active = true;
            Instantiate(cube, gameObject.transform.position + Vector3.up, Quaternion.identity);
        }
    }

}
