using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour {

    public int tutorialNumber = 1;
    bool hasActivated = false;

    public void Activate() {
    
        if (!hasActivated) {
            GameObject tutorialManager = GameObject.Find("TutorialManager");
            tutorialManager.GetComponent<TutorialManager>().Activate(tutorialNumber - 1);  
            hasActivated = true;      
        }
       
    }

}
