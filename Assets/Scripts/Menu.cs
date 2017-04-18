﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour {

    public GameObject introDoor;
    public GameObject mainMenu;
    public GameObject emitter;
    public GameObject laser;

    public GameObject textComp;
	public AudioClip putt;
    public float startDelay = 2f;
	public float typeDelay = 0.01f;

	// Use this for initialization
	void Start () {
		laser.SetActive(false);
        textComp.GetComponent<Text>().text = "";
        StartCoroutine(TypeIn("The Cave "));
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

    public IEnumerator TypeIn(string msg){
		yield return new WaitForSeconds(startDelay);
		for (int i = 0; i < msg.Length; i++){
			textComp.GetComponent<Text>().text = msg.Substring(0, i);
			GetComponent<AudioSource>().PlayOneShot(putt, 0.03f);
			yield return new WaitForSeconds(typeDelay);
		}
	}

}