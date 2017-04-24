using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryMute : MonoBehaviour {

    AudioListener audioListener;

    private void Awake() {

        audioListener = GetComponent<AudioListener>();
        AudioListener.volume = 0;
        StartCoroutine(MuteInitialAudio());
    }

    //remove audio for the beginning moments of the game
    IEnumerator MuteInitialAudio() {
        yield return new WaitForSeconds(2);
        AudioListener.volume = 1;

        audioListener.enabled = true;
    }
}
