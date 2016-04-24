using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void exit() {
        Application.Quit();
    }
    public void reset()
    {
        Application.LoadLevel("scene1");
    }
}
