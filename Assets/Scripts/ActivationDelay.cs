using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationDelay : MonoBehaviour {

    public GameObject[] lasers;
    public float waitTime = 6;

    private void Awake() {
        for(int i = 0; i < lasers.Length; i ++) {
            lasers[i].SetActive(false);
        }

        StartCoroutine(DelayActivation());
    }

	
	IEnumerator DelayActivation() {
        yield return new WaitForSeconds(waitTime);

        for(int i = 0; i < lasers.Length; i ++) {
            lasers[i].SetActive(true);
        }
    }

}
