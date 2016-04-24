using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour {
    LineRenderer line_renderer;
    public GameObject soldior;
    public GameObject supporter;
	// Use this for initialization
	void Start () {
        line_renderer = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        move_line();
	}
    void move_line()
    {
        line_renderer.SetPosition(0, soldior.transform.position);
        line_renderer.SetPosition(1, supporter.transform.position);
    }
}
