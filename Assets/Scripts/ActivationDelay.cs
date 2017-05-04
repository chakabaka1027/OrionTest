using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivationDelay : MonoBehaviour {

    public GameObject[] lasers;
    public float waitTime = 6;
    public GameObject introEmitter;

    private void Awake() {
        for(int i = 0; i < lasers.Length; i ++) {
            lasers[i].SetActive(false);
            if(lasers[i].transform.parent.GetComponent<AudioSource>() != null) { 
                lasers[i].transform.parent.GetComponent<AudioSource>().volume = 0;
            }
        }

        if (introEmitter != null) {
            introEmitter.GetComponent<AudioSource>().volume = 0;
        }

        if (SceneManager.GetActiveScene().buildIndex > 0 && SceneManager.GetActiveScene().buildIndex != 12) {
            StartCoroutine(DelayActivation());
        }
    }

	
	public IEnumerator DelayActivation() {
        yield return new WaitForSeconds(waitTime);

        for(int i = 0; i < lasers.Length; i ++) {
            lasers[i].SetActive(true);

            
            if(lasers[i].transform.parent.GetComponent<AudioSource>() != null) { 
                lasers[i].transform.parent.GetComponent<AudioSource>().volume = .1f;
            }
        }

        

        if (introEmitter != null) {
            introEmitter.GetComponent<AudioSource>().volume = .1f;
        }
    }



}
