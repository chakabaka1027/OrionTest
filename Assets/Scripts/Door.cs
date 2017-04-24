﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public AudioSource openAudioSouce;
    public AudioSource closeAudioSource;

    public Transform closedPos;
    public Transform openedPos;

    public int requiredSensorActivations = 1;
    public int currentSensorActivations = 0;

    public float time = 3;

    Vector3 closedPosition;
    Vector3 openedPosition;

    bool doorOpened = false;

    int toggle = 1;

	// Use this for initialization
	void Start () {
	    closedPosition = closedPos.position;
        openedPosition = openedPos.position;

	}
	
	void Update() {
        if(currentSensorActivations == requiredSensorActivations && !doorOpened) {
            ToggleDoor();
            doorOpened = true;
        } else if(currentSensorActivations != requiredSensorActivations && doorOpened){
            ToggleDoor();
            doorOpened = false;
        }
    }

    
    public void ToggleDoor() { 

        toggle = 1 - toggle;

        if (toggle == 1) {
            StartCoroutine("OpenDoor");
        } else {
            StartCoroutine("CloseDoor");
        }
    }
    
    
    public IEnumerator OpenDoor() {
        
        StopCoroutine("CloseDoor");
        float percent = 0;
        float speed = 1 / time;
        Vector3 currentPos = transform.position;

        while(percent < 1) {
            percent += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(currentPos, openedPosition, percent);
            
            //sound
            openAudioSouce.volume = .1f;
            closeAudioSource.volume = 0;
            
            yield return null;
        }
        openAudioSouce.volume = 0;

    }

    public IEnumerator CloseDoor() {
        StopCoroutine("OpenDoor");

        float percent = 0;
        float speed = 1 / time;
        Vector3 currentPos = transform.position;

        while(percent < 1) {
            percent += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(currentPos, closedPosition, percent);
            
            //sound
            openAudioSouce.volume = 0;
            closeAudioSource.volume = .1f;
            
            yield return null;
        }
        closeAudioSource.volume = 0;

    }
}
