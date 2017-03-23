using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scannable : MonoBehaviour {

    public void Ping() {
        GetComponent<Animator>().Play("IncreaseEmission");
    }

    private void Update() {

        if(Input.GetKeyDown(KeyCode.T)) {
            Ping();
        }
    }

}
