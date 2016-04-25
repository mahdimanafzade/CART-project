using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	
	void Start () {
	
	}
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
