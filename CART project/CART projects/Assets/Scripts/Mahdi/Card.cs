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
    public Card_state card_state;

}

