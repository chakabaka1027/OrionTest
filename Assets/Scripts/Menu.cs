using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public GameObject introDoor;
    public GameObject mainMenu;
    public GameObject emitter;
    public GameObject laser;



	// Use this for initialization
	void Start () {
		laser.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BeginAscent() {
        StartCoroutine(BeginAscentCoroutine());
    }

    IEnumerator BeginAscentCoroutine() {
        yield return new WaitForSeconds(.5f);

        mainMenu.GetComponent<Animator>().Play("Deactivate");

        yield return new WaitForSeconds(1);
        StartCoroutine(introDoor.GetComponent<Door>().OpenDoor());
        
        yield return new WaitForSeconds(4);

              
        emitter.GetComponent<Animator>().Play("Activate");

        yield return new WaitForSeconds(3f);
        laser.SetActive(true);



    }

    public void QuitGame() {
        StartCoroutine(QuitGameCoroutine());
    }

    IEnumerator QuitGameCoroutine() {
        yield return new WaitForSeconds(0.5f);
        Application.Quit();

    }

}
