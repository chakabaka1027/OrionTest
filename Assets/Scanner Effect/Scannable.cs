using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scannable : MonoBehaviour {

    public void Ping() {
        Debug.Log("pinging");
        GetComponent<Animator>().Play("IncreaseEmission");
        StartCoroutine(DecreaseEmission());
    }

    IEnumerator DecreaseEmission() {
        yield return new WaitForSeconds(7);
        GetComponent<Animator>().Play("DecreaseEmission");
    }

    

}
