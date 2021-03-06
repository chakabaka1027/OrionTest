﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour {

    float ascentDelay = 1.5f;

    public bool isOccupied = false;
    public bool isStartingElevator = false;

    public float travelTime = 5;

    public GameObject target;

    public GameObject player;

    public int nextLevelIndex;

    AudioSource audioSource;
    public AudioClip success;

    void Start() { 
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0;

        if(isStartingElevator) {
            StartCoroutine(Activate());
        }

        target.GetComponent<MeshRenderer>().enabled = false;
    }

    void Update() {
        
        if(isOccupied) {
            player.transform.position = gameObject.transform.position;

        }

    }	

    public void InitiateElevator() {

        StartCoroutine(Activate());

    }

    public IEnumerator Activate() {

        if(!isStartingElevator) { 
            yield return new WaitForSeconds(ascentDelay);
        }
        player.GetComponent<AudioSource>().PlayOneShot(success, .1f);

        Vector3 startLocation = gameObject.transform.position;
        Vector3 endLocation = target.transform.position;

        isOccupied = true;

        float percent = 0;
        float speed = 1 / travelTime;

        while (percent < 1) {
            percent += Time.deltaTime * speed;
            gameObject.transform.position = Vector3.Lerp(startLocation, endLocation, percent); 

            audioSource.volume = 0.1f;
            yield return null;
        }

        if(!GameObject.Find("IntroductoryElevator")) {
            FindObjectOfType<TutorialManager>().Activate(0);

        }
        
        //load next level
        if(!isStartingElevator) {

            //if last elevator, save final song container obj
            if(gameObject.name == "FinalElevator") {
                GameObject container = GameObject.Find("FinalSongContainer");
                DontDestroyOnLoad(container);
            }
            SceneManager.LoadScene(nextLevelIndex);
            
        }

        audioSource.volume = 0;

        isOccupied = false;
    }

}
