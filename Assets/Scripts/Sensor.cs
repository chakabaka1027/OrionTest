using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sensor : MonoBehaviour {

    public UnityEvent sensorAction;
    public bool wasTriggeredByLaser = false;

    public void Activate() {

        StopCoroutine("DeactivationTimer");
        if(!wasTriggeredByLaser) {

            wasTriggeredByLaser = true;
            sensorAction.Invoke();

            if (gameObject.transform.name == "LaserSensor") {
                GameObject cords = gameObject.transform.FindChild("Cords").gameObject;
                for(int i = 0; i < cords.transform.childCount; i++) {
                    cords.transform.GetChild(i).GetComponent<Animator>().Play("Active");
                }
                
                
                //GetComponentInChildren<Animator>().Play("Active");
            }

        }
        StartCoroutine("DeactivationTimer");

    }

    public void Deactivate() {
        if(wasTriggeredByLaser) {
            sensorAction.Invoke();
            wasTriggeredByLaser = false;

            if (gameObject.transform.name == "LaserSensor") {
                GameObject cords = gameObject.transform.FindChild("Cords").gameObject;
                //cords.transform.GetComponentInChildren<Animator>().Play("Inactive");
                for(int i = 0; i < cords.transform.childCount; i++) {
                    cords.transform.GetChild(i).GetComponent<Animator>().Play("Inactive");
                }
            }
        }
        
    }

    IEnumerator DeactivationTimer() {
        yield return new WaitForSeconds(.2f);
        Deactivate();
    }

}
