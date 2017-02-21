using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour {

    public void ChangeColorRed() {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public void ChangeColorBlue(){
        gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }
}
