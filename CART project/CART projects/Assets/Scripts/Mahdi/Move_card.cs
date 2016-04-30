using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class Move_card : MonoBehaviour {
    
    pathFinder.node final_targetNode;
    pathFinder.node current_targetNode;
    public bool sw_Stopmove=false;
    bool check = false;
    Transform _transform;
    public float speed = 1;

    
    public Card card;
    int pathindex=0;

    public Elixir_progress elixir;
	// Use this for initialization
	void Start () {
        card=GetComponent<Card>();        
        //elixir = GameManager._instance.elixir;
    
        
	}
	void OnEnable(){
        if(_transform==null){
            _transform= transform;
        }
    }

	// Update is called once per frame
	void Update () {
        print(card.card_state);
	}

    public void stop_move(){
        StopCoroutine("move_coroutine");
    }
    

    

    public void move(Vector2 target)
    {


        List<pathFinder.node> pathNods = new List<pathFinder.node>();
        
        /*	if (check) 
                pathNods = mapMacker.Instance.GetPath (id, target, true);
            else 
                pathNods = mapMacker.Instance.GetPath(new Vector2(transform.position.x,transform.position.y), target , id , true);
    */
        if (current_targetNode != null)
        {
            current_targetNode.usageID = -2;
        }
        pathNods = mapMacker.Instance.GetPath(new Vector2(transform.position.x, transform.position.y), target, card.id, true);
        
        if (pathNods.Count == 0)
            return;

        print("count: "+pathNods.Count);
        final_targetNode = pathNods[pathNods.Count - 1];
        current_targetNode = pathNods[0];
        stop_move();
        
        StartCoroutine("move_coroutine", pathNods);

    }

   


     IEnumerator move_coroutine(List<pathFinder.node> pathNods)
    {
        float dist;
        Vector2 new_nodeVec;
        Vector2 _nodeVec;
        pathFinder.node newnode;

        Vector3 path_target;

        pathindex = 0;
        print("***: "+pathNods.Count);
        check = false;

        bool success_elixir=elixir.check_elixir(pathNods.Count);
        if (success_elixir && pathNods.Count>1)
        {


            
            card.card_state = Card_state.move;
            _nodeVec = new Vector2(card._node.rowIndex, card._node.columnIndex);
            newnode = pathNods[1];

            new_nodeVec = new Vector2(newnode.rowIndex, newnode.columnIndex);

            card.set_direction(_nodeVec, new_nodeVec);
            card.play_moveAnimation();
            card._node = newnode;
            
            
            while (true)
            {
                

                path_target = new Vector3(pathNods[pathindex].pos.x, pathNods[pathindex].pos.y, _transform.position.z);
                dist = Vector3.Distance(path_target, _transform.position);

                _transform.position = Vector3.MoveTowards(_transform.position, path_target, Time.deltaTime * speed);
                if (dist < 0.1f)
                {

                    
                    
                    /*if (sw_Stopmove) { // move is stopped when the card want to attack or support
                        sw_Stopmove = false;
                        break;
                    }*/

                    if (pathindex < pathNods.Count - 2)
                    {
                        pathindex++;
                        current_targetNode = pathNods[pathindex];
                    }
                    else if (pathindex == pathNods.Count - 2) {
                        pathindex++;
                        current_targetNode = pathNods[pathindex];
                       /* if(){

                        }*/
                    }
                    else
                    {
                        //   _transform.position = card._node.pos;
                        check = true;
                        break;
                    }

                    _nodeVec = new Vector2(card._node.rowIndex, card._node.columnIndex);
                    newnode = pathNods[pathindex];
                    new_nodeVec = new Vector2(newnode.rowIndex, newnode.columnIndex);

                    card.set_direction(_nodeVec, new_nodeVec);
                    card.play_moveAnimation();
                    card._node = newnode;

                }

                yield return null;
            }
            
        }
        card.card_state = Card_state.none;
        card.play_idleAnimation();
    }


    
}



