using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslocatorSpawner : MonoBehaviour {

	public GameObject ball;
    public int ballCount = 1;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            SpawnBalls();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            ReloadAmmo();
        }
    }

    public void SpawnBalls() {
        if (ballCount == 1) {
            Shoot();
            ballCount = ballCount - 1;

        }
    }
    
    void Shoot() {
        Instantiate(ball, gameObject.transform.position, Quaternion.identity);
    }

    public void ReloadAmmo() {
        ballCount = 1;
    }

}
