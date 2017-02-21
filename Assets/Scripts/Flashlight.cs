using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {
        
    int flashlightValue = 0;


    public void FlashLightOn() {
        gameObject.SetActive(true);
    }

    public void FlashLightOff() {
        gameObject.SetActive(false);
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
