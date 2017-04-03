using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLantern : MonoBehaviour {

    public int activationSwitch = 0;
    public bool isActive = false;

    public bool isCurrentLantern = false;

    public GameObject sensor;
    GameObject endLocTarget;
 
    Vector3 startLocation;
    Vector3 endLocation;

    bool wasTriggeredByLaser = false;

    private void Start() {

    //deactivate mesh renderer if the player is occupying this lantern
       

        startLocation = sensor.transform.position;
        endLocTarget = transform.parent.FindChild("Lantern").gameObject;
        endLocation = endLocTarget.transform.position;
        //sensor.transform.position + Vector3.up * 1f;


        if(gameObject.name == "MovementLanternActual") { 
            if(!isCurrentLantern) {
                gameObject.GetComponent<MeshRenderer>().enabled = true;

            }

            StartCoroutine(MoveFromSensor());
        }
        if(gameObject.name != "MovementLanternActual") {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;

            gameObject.GetComponentInChildren<Light>().enabled = false;
        }
    }


    public void SpawnLantern() {
        GameObject childLantern = Instantiate(gameObject, startLocation, Quaternion.identity) as GameObject;
        childLantern.name = "MovementLanternActual";
        childLantern.transform.parent = gameObject.transform.parent;

        childLantern.GetComponent<MovementLantern>().isActive = true;
        
        
    }

    public void DestroyLantern() {
        GameObject childLantern = gameObject.transform.parent.FindChild("MovementLanternActual").gameObject;
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
            Debug.Log("hit");    

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
            gameObject.transform.GetComponent<BoxCollider>().enabled = true;


    }
}
