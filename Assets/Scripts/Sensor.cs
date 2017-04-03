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

        }
        StartCoroutine("DeactivationTimer");

    }

    public void Deactivate() {
        if(wasTriggeredByLaser) {
            sensorAction.Invoke();
            wasTriggeredByLaser = false;
        }
        
    }

    IEnumerator DeactivationTimer() {
        yield return new WaitForSeconds(.2f);
        Deactivate();
    }

}
