using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public static GameManager _instance;
	//public Elixir_progress elixir;
    public List<GameObject> soldior_enemies;
    public List<GameObject> supporter_enemies;
    public List<GameObject> soldior_cards;
    public List<GameObject> supporter_cards;

    public void remove_FromGameManager(GameObject dead_card){
        if(dead_card.tag=="soldior_card"){
            soldior_cards.Remove(dead_card);
        }
        else if (dead_card.tag == "supporter_card")
        {
            print("ttt");
            supporter_cards.Remove(dead_card);
        }
        else if (dead_card.tag == "soldior_enemy")
        {
            soldior_enemies.Remove(dead_card);
        }
        else if (dead_card.tag == "supporter_enemy")
        {
            supporter_enemies.Remove(dead_card);
        }
    }

	// Use this for initialization

	void Start () {
		_instance=this;
        //elixir = GameObject.FindGameObjectWithTag("Elixir").GetComponent<Elixir_progress>();
        set_cards();
        set_enemies();
        


        
	}
    void set_enemies() { 
        int i;
        GameObject[] soldior_enems=GameObject.FindGameObjectsWithTag("soldior_enemy");
        GameObject[] supporter_enems=GameObject.FindGameObjectsWithTag("supporter_enemy");
        for(i=0;i<soldior_enems.Length;i++){
            soldior_enemies.Add(soldior_enems[i]);
        }

        for (i = 0; i < supporter_enems.Length; i++)
        {
            supporter_enemies.Add(supporter_enems[i]);
        }
        
    }

    void set_cards()
    {
        int i;
        GameObject[] soldior_cs = GameObject.FindGameObjectsWithTag("soldior_card");
        GameObject[] supporter_cs = GameObject.FindGameObjectsWithTag("supporter_card");
        for (i = 0; i < soldior_cs.Length; i++)
        {
            soldior_cards.Add(soldior_cs[i]);
        }
        for (i = 0; i < supporter_cs.Length; i++)
        {
            supporter_cards.Add(supporter_cs[i]);
        }

    }

	// Update is called once per frame
	void Update () {
	
	}
}
