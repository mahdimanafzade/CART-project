using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AI_enemy : MonoBehaviour {

    
    
    Card card;
    Attack_card attack_card;
    Move_card move_card;
    
    public Transform _transform;

	// Use this for initialization
	void Start () {
        attack_card = GetComponent<Attack_card>();
        move_card = GetComponent<Move_card>();
        card=GetComponent<Card>();
	    _transform=transform;
        
	}
    

    void attackAI(GameObject card_obj)
    {
        attack_card.attack(card_obj.GetComponent<Health>());

    }

    void move_ToClosestCard()
    {
        
        if (attack_card.closest_card != null)
        {
            
        /*    List<pathFinder.node> pathNods = new List<pathFinder.node>();
            pathNods = mapMacker.Instance.GetPath(new Vector2(_transform.position.x, _transform.position.y), attack_card.closest_card.transform.position, card.id, false);

            pathNods.RemoveAt(pathNods.Count - 1);

            if (pathNods.Count == 0)
            {
                return;
            }            
            move_card.move();*/
            
        }
    }
    
    
	// Update is called once per frame
	void Update () {
        if (card.card_state != Card_state.death)
        {
            if (attack_card.closest_card != null)
            {
                if (card.card_state == Card_state.none)
                {
                    print("2222");
                    card.card_state = Card_state.move;
                    move_ToClosestCard();
                }
            }
        }
        
        
        
	}
    
}
