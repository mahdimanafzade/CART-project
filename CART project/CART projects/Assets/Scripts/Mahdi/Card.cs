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
    Animator _animator;
    
    public int id = 0;
    public Direction direction=Direction.right;

    public Card_state card_state;
    public bool selected_card = false;
    public GameObject fx_active;
    public pathFinder.node _node;

    float up_val=0;
    float right_val=0;
    void Start() {
        _animator=GetComponent<Animator>();
        card_state = Card_state.none;
        play_idleAnimation();


        fx_active = transform.GetChild(0).gameObject;
        _node=mapMacker.Instance.GetNode(id);
    }

    public void set_direction(Vector2 node_vec,Vector2 new_nodeVec)
    {
        Vector2 diff = (new_nodeVec - node_vec);
        if (diff.x > 0 && diff.y > 0) {
            direction = Direction.down_right;
            up_val = -0.7f;
            right_val = 0.7f;
        }
        else if (diff.x > 0 && diff.y < 0)
        {
            direction = Direction.down_left;
            up_val = -0.7f;
            right_val = -0.7f;
        }
        else if (diff.x > 0 && diff.y == 0)
        {
            direction = Direction.down;
            up_val = -1f;
            right_val = 0f;
        }
        else if (diff.x == 0 && diff.y > 0)
        {
            direction = Direction.right;
            up_val = 0;
            right_val = 1;
        }
        else if (diff.x == 0 && diff.y < 0)
        {
            direction = Direction.left;
            up_val = 0;
            right_val = -1;
        }        
        else if (diff.x < 0 && diff.y > 0)
        {
            direction = Direction.up_right;
            up_val = 0.7f;
            right_val = 0.7f;
        }
        else if (diff.x < 0 && diff.y < 0)
        {
            direction = Direction.up_left;
            up_val = 0.7f;
            right_val = -0.7f;
        }
        else if (diff.x < 0 && diff.y == 0)
        {
            direction = Direction.up;
            up_val = 1;
            right_val = 0;
        }


    }

    public void play_idleAnimation()
    {
        print("***");
        _animator.SetFloat("up",up_val);
        _animator.SetFloat("right",right_val);
        _animator.SetInteger("animation", 0);
    }
    public void play_moveAnimation()
    {
        _animator.SetFloat("up", up_val);
        _animator.SetFloat("right", right_val);
        _animator.SetInteger("animation", 1);
    }
    public void play_attackAnimation()
    {
        _animator.SetFloat("up", up_val);
        _animator.SetFloat("right", right_val);
        _animator.SetInteger("animation", 2);
    }     
    void Update() {
	
    
    }
}

