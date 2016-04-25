// Developer:Mahdi Manafzade
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum Card_state
{
    none, death, attack, move
}
public class Card:MonoBehaviour {
    public int id = 0;
    public Card_state card_state;
    public bool selected_card = false;
    public GameObject fx_active;
    void Start() {
        fx_active = transform.GetChild(0).gameObject;
    }
}

