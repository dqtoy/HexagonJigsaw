using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreLoading : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Resources.Load("scenes/Main");
        //this.gameObject.scene
        SceneManager.LoadScene("Resources/scenes/Main");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
