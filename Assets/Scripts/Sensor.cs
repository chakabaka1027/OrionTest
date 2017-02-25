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
            sensorAction.Invoke();
            wasTriggeredByLaser = true;
        }
        StartCoroutine("DeactivationTimer");

    }

    public void Deactivate() {
        if(wasTriggeredByLaser) {
            sensorAction.Invoke();
            wasTriggeredByLaser = false;
        }
        
    }

    public IEnumerator DeactivationTimer() {
        yield return new WaitForSeconds(.1f);
        Deactivate();
    }

}
