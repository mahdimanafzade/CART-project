using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class Move_card : MonoBehaviour {
    
    bool check=false;
    Card card;
    float _speed;
    
    Transform _transform;
    int _id;
    int pathindex=0;
    Elixir_progress elixir;
	// Use this for initialization
	void Start () {
        card=GetComponent<Card>();
        elixir = GameManager._instance.elixir;
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
    public void move_mode(int id,Vector2 target,ref float speed){
        if (!check) { 
            move(target,ref speed);
        }
        else{
            move(id,target,ref speed);
        }
    }

    public void move(int id,Vector2 target,ref float speed)
    {
		print ("**************************");        
        _speed=speed;

        print(id);
        print(target);

        stop_move();
        List<Vector2> pathNods = mapMacker.Instance.GetPath(id, target, true);

        StopCoroutine("move_coroutine");
        StartCoroutine("move_coroutine", pathNods);

    }

    
    public void move(Vector2 target, ref float speed)
    {
        print("**************************");
        
        _speed = speed;


        stop_move();
        List<Vector2> pathNods = mapMacker.Instance.GetPath(transform.position, target, true);

        StopCoroutine("move_coroutine");
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

                _transform.position = Vector3.MoveTowards(_transform.position, path_target, Time.deltaTime * _speed);
                if (dist < 0.1f)
                {
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
            card.card_state = Card_state.none;
        }
    }


}
