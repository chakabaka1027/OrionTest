using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	Rigidbody rb;

    void Start(){
		rb = GetComponent<Rigidbody>();
		rb.AddForce(Camera.main.gameObject.transform.forward * 30, ForceMode.Impulse);
	}


}
