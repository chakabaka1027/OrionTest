using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scannable : MonoBehaviour {

    bool hasBeenScanned;

    public void Ping() {
        if(!hasBeenScanned) {
            hasBeenScanned = true;
            Debug.Log("pinging");
            if(gameObject.name == "MirrorX" || gameObject.name == "MirrorY") {
                GetComponent<Animator>().Play("IncreaseMoveableMirrorEmission");
            } else {
                GetComponent<Animator>().Play("IncreaseEmission");
            }
            StartCoroutine(DecreaseEmission());
        }
        
    }

    IEnumerator DecreaseEmission() {
        yield return new WaitForSeconds(30);
        GetComponent<Animator>().Play("DecreaseEmission");
        hasBeenScanned = false;
    }

    

}
