using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialZoneTrigger : MonoBehaviour {

    public int tutorialNumber = 1;
    bool hasActivated = false;


    private void OnTriggerEnter(Collider other) {
        if(GameObject.FindGameObjectWithTag("Player") && !hasActivated) {
            GameObject tutorialManager = GameObject.Find("TutorialManager");
            tutorialManager.GetComponent<TutorialManager>().Activate(tutorialNumber - 1);  
            hasActivated = true;      
        }
    }

}
