using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour {

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
        audioSource.PlayOneShot(success, .1f);

        StartCoroutine(Activate());

    }

    public IEnumerator Activate() {

        Vector3 startLocation = gameObject.transform.position;
        Vector3 endLocation = target.transform.position;

        isOccupied = true;

        float percent = 0;
        float speed = 1 / travelTime;

        while (percent < 1) {
            percent += Time.deltaTime * speed;
            gameObject.transform.position = Vector3.Lerp(startLocation, endLocation, percent); 

            yield return null;
        }

        FindObjectOfType<TutorialManager>().Activate(0);
        

        if(!isStartingElevator) {
            SceneManager.LoadScene(nextLevelIndex);
        }

        isOccupied = false;
    }

}
