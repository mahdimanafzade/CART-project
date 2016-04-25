// Developer:Mahdi Manafzade
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



public class Support_card : MonoBehaviour
{
    public SpecialType specialType;
    public Card card;
    Move_card move_card;
    Dictionary<int, int> line_dic = new Dictionary<int, int>();
    LineRenderer line_renderer;
    Line line_script;    

    public int support_num = 0;
    public int support_num_limit = 2;
    public int range=2;
    

    


   
    

    // Use this for initialization
    void Start()
    {
        card=GetComponent<Card>();
        card.card_state = Card_state.none;
        move_card=GetComponent<Move_card>();
        

        //attack_interval = 1f / (float)attack_ratio;
    }

    /*public void move(Vector2 target)
    {
        //print(target);
        //print(supporter_Card_property.id);
        //print(supporter_Card_property.speed);

        card.card_state = Card_state.move;

        move_card.move(target);
        
    }*/

    // Update is called once per frame
    void Update()
    {    
    }
    
    public void support(GameObject soldior_cardObj)
    {
        Attack_card soldior_card = soldior_cardObj.GetComponent<Attack_card>();

        if (line_dic.ContainsKey(soldior_card.card.id))
        {
            
            transform.GetChild(2).GetChild(line_dic[soldior_card.card.id]).gameObject.SetActive(false);
            line_dic.Remove(soldior_card.card.id);
        }
        else if (support_num < support_num_limit && soldior_card.support_num < soldior_card.support_num_limit)
        {

            move_card.stop_move();

            if (specialType ==SpecialType.supporter_monk)
            {
                StartCoroutine("support_hpimprove_coroutine", soldior_cardObj);
            }
            else if (specialType==SpecialType.supporter_ap)
            {
                StartCoroutine("support_ap_coroutine", soldior_cardObj);
            }
            
        }
    } // main support function

    void add_support(Attack_card soldior_card) {
        support_num++;
        soldior_card.support_num++;

        if (line_dic.Count == 0 || (line_dic.Count == 1 && line_dic.ContainsValue(1)))
        {
            print("0");
            line_dic.Add(soldior_card.card.id, 0);

            transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            line_renderer = transform.GetChild(2).GetChild(0).GetComponent<LineRenderer>();
            line_script = transform.GetChild(2).GetChild(0).GetComponent<Line>();

            line_script.soldior = soldior_card.gameObject;
            line_script.supporter = gameObject;
        }
        else if (line_dic.Count == 1 && line_dic.ContainsValue(0))
        {
            print("1");
            line_dic.Add(soldior_card.card.id, 1);

            transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
            line_renderer = transform.GetChild(2).GetChild(1).GetComponent<LineRenderer>();

            line_script = transform.GetChild(2).GetChild(1).GetComponent<Line>();
            line_script.soldior = soldior_card.gameObject;
            line_script.supporter = gameObject;
        }
    }   // add support to soldior
    void remove_support(Attack_card soldior_card)
    {
        soldior_card.support_num--;
        support_num--;
        if (line_dic.ContainsKey(soldior_card.card.id))
        {
            transform.GetChild(2).GetChild(line_dic[soldior_card.card.id]).gameObject.SetActive(false);
            line_dic.Remove(soldior_card.card.id);
        }
    } // remove support from soldior

    IEnumerator support_hpimprove_coroutine(GameObject soldior_cardObj)
    {
        Attack_card soldior_card = soldior_cardObj.GetComponent<Attack_card>();
        Health health_soldior_card = soldior_cardObj.GetComponent<Health>();
        float dis = Vector2.Distance(transform.position, soldior_card.transform.position);
        if (dis <= range)
        {
            add_support(soldior_card);
            while (dis <= range && line_dic.ContainsKey(soldior_card.card.id))
            {
                dis = Vector2.Distance(transform.position, soldior_card.transform.position);
                improve_health(5f, health_soldior_card);
                
                yield return null;
            }

            remove_support(soldior_card);

        }
    } // special support coroutine(monk)
    void improve_health(float improve_health, Health health_soldiorCard)
    {
        if (health_soldiorCard.current_health < health_soldiorCard.health_base)
        {
            health_soldiorCard.current_health += (improve_health * Time.deltaTime);
        }
        else
        {
            health_soldiorCard.current_health = health_soldiorCard.health_base;
        }

    }

    IEnumerator support_ap_coroutine(GameObject soldior_cardObj)
    {
        Attack_card soldior_card = soldior_cardObj.GetComponent<Attack_card>();

        float dis = Vector2.Distance(transform.position, soldior_card.transform.position);
        if (dis <= range)
        {
            add_support(soldior_card);            
            soldior_card.attack_point += 10;
            
            while (dis <= range && line_dic.ContainsKey(soldior_card.card.id))
            {
                dis = Vector2.Distance(transform.position, soldior_card.transform.position);                
                yield return null;
            }
            soldior_card.attack_point -= 10;
            

            remove_support(soldior_card);

        }
    }       // special support coroutine(ap increase)
}

public enum SpecialType
{
    supporter_monk, supporter_ap, supporter_hp
}



