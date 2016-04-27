using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class Move_card : MonoBehaviour {
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
        
	}

    public void stop_move(){
        StopCoroutine("move_coroutine");
    }
    

    public void move(List<Vector2> pathNods)
    {
    
        
        if (pathNods.Count == 0)
        {
            return;
        }
        stop_move();
        card.card_state = Card_state.move;
        StartCoroutine("move_coroutine", pathNods);
    }

    public void move(Vector2 target)
    {
        
        
        List<Vector2> pathNods = new List<Vector2>();
        print("ccccccccccccccc   :   " + check);
        /*	if (check) 
                pathNods = mapMacker.Instance.GetPath (id, target, true);
            else 
                pathNods = mapMacker.Instance.GetPath(new Vector2(transform.position.x,transform.position.y), target , id , true);
    */
        pathNods = mapMacker.Instance.GetPath(new Vector2(transform.position.x, transform.position.y), target, card.id, true);

        if (pathNods.Count == 0)
            return;

        stop_move();
        card.card_state = Card_state.move;
        StartCoroutine("move_coroutine", pathNods);

    }

   


     IEnumerator move_coroutine(List<Vector2> pathNods)
    {
        float dist;
        
        Vector3 path_target;

        pathindex = 0;
        print("***");
        check = false;
        bool success_elixir=elixir.check_elixir(pathNods.Count);
		if (success_elixir)
        {
            while (true)
            {

                path_target = new Vector3(pathNods[pathindex].x, pathNods[pathindex].y, _transform.position.z);
                dist = Vector3.Distance(path_target, _transform.position);

                _transform.position = Vector3.MoveTowards(_transform.position, path_target, Time.deltaTime * speed);
                if (dist < 0.1f)
                {
                    
                    card._node = mapMacker.Instance.GetNode(card.id);

                    /*if (sw_Stopmove) { // move is stopped when the card want to attack or support
                        sw_Stopmove = false;
                        break;
                    }*/
                    if (pathindex < pathNods.Count - 1)
                        pathindex++;
                    else
                    {
                        
                        check = true;
                        break;
                    }
                }

                yield return null;
            }
            
        }
        card.card_state = Card_state.none;
    }


    
}



