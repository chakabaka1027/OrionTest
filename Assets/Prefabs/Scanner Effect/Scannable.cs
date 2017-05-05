using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scannable : MonoBehaviour {

    bool hasBeenScanned;
    bool wasTriggeredByLaser = false;
    public AudioSource pingAudioSource;
    public AudioSource rotationAudioSource;
    public AudioClip laserImpact;

    void Start() {
    }

    public void Ping() {
        if(!hasBeenScanned) {
            hasBeenScanned = true;
            //Debug.Log("pinging");
            if(gameObject.name == "MirrorX" || gameObject.name == "MirrorY") {
                //GetComponent<Animator>().Play("IncreaseMoveableMirrorEmission");
                GetComponent<Animator>().Play("IncreaseEmission");

            } else {
                GetComponent<Animator>().Play("IncreaseEmission");
            }
            StartCoroutine(DecreaseEmission());
        }
        
    }

    IEnumerator DecreaseEmission() {
        yield return new WaitForSeconds(5);
        GetComponent<Animator>().Play("DecreaseEmission");
        hasBeenScanned = false;
    }

    public void Activate() {

        StopCoroutine("DeactivationTimer");
        if(!wasTriggeredByLaser) {
            wasTriggeredByLaser = true;
            pingAudioSource.PlayOneShot(laserImpact, 0.5f);
        }
        StartCoroutine("DeactivationTimer");

    }

    public void Deactivate() {
        if(wasTriggeredByLaser) {           
            wasTriggeredByLaser = false;
        }
        
    }

    IEnumerator DeactivationTimer() {
        yield return new WaitForSeconds(.2f);
        Deactivate();
    }

    

}
