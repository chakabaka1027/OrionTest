using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongDestroyingElevator : MonoBehaviour {

	public IEnumerator StopEndGameMusic() {
        Debug.Log("End");
        GameObject endSongContainer = GameObject.Find("FinalSongContainer");
        
        float percent = endSongContainer.GetComponent<AudioSource>().volume;
        float time = 2;
        float speed = 1 / time;
        while (percent > 0) {
            percent -= Time.deltaTime * speed;
            endSongContainer.GetComponent<AudioSource>().volume = percent;
            yield return null;
        }
        Destroy (endSongContainer);
    }
}
