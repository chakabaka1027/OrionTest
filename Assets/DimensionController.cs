using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DimensionController : MonoBehaviour {
    
    void Awake() {
        SceneManager.LoadScene(6, LoadSceneMode.Additive);
    }

}
