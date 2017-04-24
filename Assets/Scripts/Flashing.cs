using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashing : MonoBehaviour {

    AudioSource audioSource;
    public AudioClip flicker;
    Light light;
    GameObject lightMaterial;
    Color lightMaterialColor;
    GameObject beam;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        light = GetComponent<Light>();
        lightMaterial = gameObject.transform.parent.FindChild("LightCasing").gameObject;
        lightMaterialColor = lightMaterial.GetComponent<MeshRenderer>().material.GetColor("_EmissionColor");
        beam = gameObject.transform.parent.FindChild("Beam").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.value > 0.9) {
            if (light.enabled) {
                //turn off light and casing for light
                light.enabled = false;
                lightMaterial.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", lightMaterialColor * 0);
                //turn off beam
                beam.GetComponent<Light>().enabled = false;
                beam.GetComponent<LightShafts>().enabled = false;

            } else {
                //turn on light and casing for light
                audioSource.PlayOneShot(flicker, .5f);
                light.enabled = true;
                lightMaterial.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", lightMaterialColor * .375f);
                //turn on beam
                beam.GetComponent<Light>().enabled = true;
                beam.GetComponent<LightShafts>().enabled = true;
            }
        }
	}
}
