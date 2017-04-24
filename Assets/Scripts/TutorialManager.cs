using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public GameObject[] tutorial;
    //public bool[] tutorialPlayed;
    public string[] tutorialText;

	public void Activate(int i) {
        tutorial[i].GetComponent<Animator>().Play("Active");
        StartCoroutine(tutorial[i].transform.FindChild("UIElementsPanel").FindChild("Text").GetComponent<Typing>().TypeIn(tutorialText[i]));
        //tutorialPlayed[i] = true;
    }

    public void Deactivate(int i) {
        tutorial[i].GetComponent<Animator>().Play("Inactive");
        //tutorialPlayed[i] = true;
    }

}
