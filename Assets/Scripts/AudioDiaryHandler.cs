using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDiaryHandler : MonoBehaviour {

    public AudioSource diary;
    public AudioSource song;

    public float diaryWait = 2;

	// Use this for initialization
	void Start () {
		StartCoroutine(Play());
	}
	
    IEnumerator Play() {
        yield return new WaitForSeconds(diaryWait);
        diary.Play();
    }

    public IEnumerator StopMusic() {
        
        float percent = song.volume;
        float time = 2;
        float speed = 1 / time;
        while (percent > 0) {
            percent -= Time.deltaTime * speed;
            song.volume = percent;
            yield return null;
        }
    }


}
