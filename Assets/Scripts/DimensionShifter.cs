using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionShifter : MonoBehaviour {

    public GameObject dimensionChangeObj;
    int toggle = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
            Shift();
        }
	}

    public void Shift() {
    
        toggle = 1 - toggle;

        if (toggle == 1) {
            gameObject.transform.position = gameObject.transform.position + Vector3.right * 100;
        } else if (toggle == 0) {
            gameObject.transform.position = gameObject.transform.position + Vector3.right * -100;
        }
    

    //for(int i = 0; i < dimensionChangeObj.transform.childCount; i++) {

    //    if (toggle == 1) {
    //        dimensionChangeObj.transform.GetChild(i).gameObject.SetActive(true);
    //    } else if (toggle == 0) {
    //        dimensionChangeObj.transform.GetChild(i).gameObject.SetActive(false);
    //    }
    //}

    }
}
