using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {
    
    public GameObject player;

    public AudioClip on;
    public AudioClip off;
    
    int flashlightValue = 0;

    void Start() {
    }

    public void FlashLightOn() {
        player.GetComponent<AudioSource>().PlayOneShot(on, .4f);
        gameObject.SetActive(true);
        if(GameObject.Find("HandAttachments-L(Clone)") != null) {
            GameObject.Find("HandAttachments-L(Clone)").transform.FindChild("Palm").transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void FlashLightOff() {
        player.GetComponent<AudioSource>().PlayOneShot(off, .4f);
        gameObject.SetActive(false);

        if(GameObject.Find("HandAttachments-L(Clone)") != null) {
            GameObject.Find("HandAttachments-L(Clone)").transform.FindChild("Palm").transform.GetChild(0).gameObject.SetActive(false);;
        }
    }

    public void FlashLightToggle() {
        flashlightValue = 1 - flashlightValue;
        if (flashlightValue == 1) {
            gameObject.SetActive(true);
        } else if (flashlightValue == 0) {
            gameObject.SetActive(false);
        }

    }
}
