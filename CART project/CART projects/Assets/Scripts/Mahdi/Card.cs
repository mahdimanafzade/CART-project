// Developer:Mahdi Manafzade
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum Card_state
{
    none, death, attack, move
}

public enum Direction
{
    left,up,down,right,up_right,up_left,down_right,down_left
}

enum Type_card { 
    supporter,soldior,spell
}
public class Card:MonoBehaviour {
    
    
    public int id = 0;
    public Direction direction;
    public Card_state card_state;
    public bool selected_card = false;
    public GameObject fx_active;
    public pathFinder.node _node;
    void Start() {
        
        fx_active = transform.GetChild(0).gameObject;
        _node=mapMacker.Instance.GetNode(id);
    }

    public void play_idleAnimation()
    {
        if (direction == Direction.down || direction == Direction.down_left
            || direction == Direction.down_right)
        {

        }
        else if (direction == Direction.up || direction == Direction.up_left
            || direction == Direction.up_right)
        {

        }
        else if (direction == Direction.right)
        {

        }
        else  // left
        {

        }
    }
    public void play_moveAnimation()
    {
        if ( direction == Direction.down)
        {

        }
        else if ( direction == Direction.up)
        {

        }
        else if ( direction == Direction.left)
        {

        }
        else if ( direction == Direction.right)
        {

        }
        else if ( direction == Direction.up_right)
        {

        }
        else if ( direction == Direction.up_left)
        {

        }
        else if ( direction == Direction.down_left)
        {

        }
        else if ( direction == Direction.down_right)
        {

        }

    }
    public void play_attackAnimation()
    {
        if (direction == Direction.down)
        {

        }
        else if ( direction == Direction.up)
        {

        }
        else if ( direction == Direction.left)
        {

        }
        else if ( direction == Direction.right)
        {

        }
        else if ( direction == Direction.up_right)
        {

        }
        else if ( direction == Direction.up_left)
        {

        }
        else if ( direction == Direction.down_left)
        {

        }
        else if ( direction == Direction.down_right)
        {

        }

    }     
    void Update() {
        if (card_state == Card_state.none) {
            play_idleAnimation();
        }
        else if (card_state == Card_state.move)
        {
            play_moveAnimation();
        }
        else if (card_state == Card_state.attack)
        {
            play_attackAnimation();
        }

        
    }
}

