﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Text))]
public class Typing : MonoBehaviour {

	string verticalMSG = "Vertical Rotation";
    string horizontalMSG = "Horizontal Rotation";

	private Text textComp;
	public float startDelay = 2f;
	public float typeDelay = 0.01f;
	public AudioClip putt;

	// Use this for initialization
	void Start () {
		//StartCoroutine("TypeIn");
	}

	void Awake(){
		textComp = GetComponent<Text>();
	}

	public IEnumerator TypeIn(string msg){
		yield return new WaitForSeconds(startDelay);
		for (int i = 0; i < msg.Length; i++){
			textComp.text = msg.Substring(0, i);
			GetComponent<AudioSource>().PlayOneShot(putt, 0.03f);
			yield return new WaitForSeconds(typeDelay);
		}
	}

	public IEnumerator TypeOff(string msg){
		for(int i =msg.Length; i >= 0; i--){
			textComp.text = msg.Substring(0, i);
			yield return new WaitForSeconds(typeDelay);
		}
	}
}