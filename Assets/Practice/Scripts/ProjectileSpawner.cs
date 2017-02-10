using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour {

	public GameObject ball;

    public void SpawnBalls() {
        StartCoroutine(ShootDelay());
    }
    
    IEnumerator ShootDelay() {
        while(true) {
            Instantiate(ball, gameObject.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
