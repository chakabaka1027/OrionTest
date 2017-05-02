using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sensor : MonoBehaviour {

    public UnityEvent sensorAction;
    public bool wasTriggeredByLaser = false;
    public GameObject door;
    public GameObject otherlaserSensor;

    AudioSource audioSource;
    public AudioClip sensorOn;
    public AudioClip sensorOff;

    void Start() {
        audioSource = GetComponent<AudioSource>();

    }

    public void Activate() {
        if(gameObject.transform.name == "DimensionSensor") {
            Debug.Log("Teleport");
        }
        StopCoroutine("DeactivationTimer");
        if(!wasTriggeredByLaser) {

            wasTriggeredByLaser = true;

            if(audioSource != null) {
                audioSource.PlayOneShot(sensorOn, .25f);
            }
            if(gameObject.transform.name != "LaserSensor") {

                sensorAction.Invoke();
            }

            if (gameObject.transform.name == "LaserSensor") {
                GameObject cords = gameObject.transform.FindChild("Cords").gameObject;
                for(int i = 0; i < cords.transform.childCount; i++) {
                    cords.transform.GetChild(i).GetComponent<Animator>().Play("Active");
                }
                //add 1 to the current sensor activations thats unique to door sensors
                
                if(door != null) {
                    door.GetComponent<Door>().currentSensorActivations += 1;

                }
                

            }

        }
        if(otherlaserSensor != null) {
            otherlaserSensor.transform.FindChild("LaserSensor").GetComponent<Sensor>().Activate();
        }
        StartCoroutine("DeactivationTimer");

    }

    public void Deactivate() {
        if(wasTriggeredByLaser) {
            if(gameObject.transform.name != "LaserSensor" && gameObject.transform.name != "DimensionSensor"){
                sensorAction.Invoke();
            }
            wasTriggeredByLaser = false;

            if(audioSource != null) {
                audioSource.PlayOneShot(sensorOff, .25f);
            }


            if (gameObject.transform.name == "LaserSensor") {
                GameObject cords = gameObject.transform.FindChild("Cords").gameObject;
                //cords.transform.GetComponentInChildren<Animator>().Play("Inactive");
                for(int i = 0; i < cords.transform.childCount; i++) {
                    cords.transform.GetChild(i).GetComponent<Animator>().Play("Inactive");
                }
                //subtract 1 to the current sensor activations thats unique to door sensors
                door.GetComponent<Door>().currentSensorActivations -= 1;

            }
        }
        
    }

    IEnumerator DeactivationTimer() {
        yield return new WaitForSeconds(.2f);
        Deactivate();
    }

}
