using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager _instance;
	public Elixir_progress elixir;
	// Use this for initialization
	void Start () {
		_instance=this;
        elixir = GameObject.FindGameObjectWithTag("Elixir").GetComponent<Elixir_progress>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
