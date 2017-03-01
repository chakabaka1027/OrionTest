using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLantern : MonoBehaviour {

    public int activationSwitch = 0;
    public bool isActive = false;

    public GameObject sensor;

 
    Vector3 startLocation;
    Vector3 endLocation;


    private void Start() {
        startLocation = sensor.transform.position;
        endLocation = sensor.transform.position + Vector3.up * 1;
        if(gameObject.name == "MovementLanternActual") {
            StartCoroutine(MoveFromSensor());
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
        if(gameObject.name != "MovementLanternActual") {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponentInChildren<Light>().enabled = false;
        }
    }


    public void SpawnLantern() {
        GameObject childLantern = Instantiate(gameObject, startLocation, Quaternion.identity) as GameObject;
        childLantern.name = "MovementLanternActual";
        childLantern.transform.parent = gameObject.transform;
        childLantern.GetComponent<MovementLantern>().isActive = true;
    }

    public void DestroyLantern() {
        GameObject childLantern = gameObject.transform.FindChild("MovementLanternActual").gameObject;
        Destroy(childLantern);
        
    }

     public void MovementLanternSwitch() {
        activationSwitch = 1 - activationSwitch;

        if(activationSwitch == 1) {
            SpawnLantern();

        } else if (activationSwitch == 0) {
            //gameObject.SetActive(false);
            //gameObject.GetComponent<MeshRenderer>().enabled = false;
            DestroyLantern();
        }

    }


    public IEnumerator MoveFromSensor() {
        
            gameObject.transform.position = startLocation;

            float percent = 0;
            float time = 1;
            float speed = 1 / time;

            while (percent < 1) {
                percent += Time.deltaTime * speed;
                gameObject.transform.position = Vector3.Lerp(startLocation, endLocation, percent); 

                yield return null;
            }
            gameObject.transform.GetChild(0).GetComponent<Light>().enabled = true;

    }
}
