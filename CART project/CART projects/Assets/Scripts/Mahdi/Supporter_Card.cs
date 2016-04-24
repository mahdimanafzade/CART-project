// Developer:Mahdi Manafzade
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



public class Supporter_Card : MonoBehaviour
{
    public Card card;
    Move_card move_card;
    public Supporter_Card_property supporter_Card_property;

    


    Dictionary<int, int> line_dic = new Dictionary<int, int>();
    LineRenderer line_renderer;
    Line line_script;    

    public Image green_health;

    
    public bool death_active = false;

    public float current_health;
    public int support_num = 0;
    public int support_num_limit = 2;

    public bool selected_card = false;

    public GameObject fx_active;


   
    

    // Use this for initialization
    void Start()
    {
        card=GetComponent<Card>();
        move_card=GetComponent<Move_card>();
        current_health = supporter_Card_property.health_base;
        card.card_state = Card_state.none;

        fx_active = transform.GetChild(0).gameObject;
        green_health = transform.FindChild("red").FindChild("green").gameObject.GetComponent<Image>();


        


        //attack_interval = 1f / (float)attack_ratio;
    }

    public void move(Vector2 target)
    {
        //print(target);
        //print(supporter_Card_property.id);
        //print(supporter_Card_property.speed);

        card.card_state = Card_state.move;

        move_card.move_mode(supporter_Card_property.id,target, ref supporter_Card_property.speed);
        
    }

    // Update is called once per frame
    void Update()
    {
        display_health();
        if (card.card_state == Card_state.death && !death_active)
        {
            Destroy(gameObject, 0.2f);
            death_active = true;
        }
    }
    void display_health()
    {
        green_health.fillAmount = (float)current_health / (float)supporter_Card_property.health_base;
    }
    public void support(Soldior_Card soldior_card)
    {
        if (line_dic.ContainsKey(soldior_card.soldior_Card_property.id))
        {
            
            transform.GetChild(2).GetChild(line_dic[soldior_card.soldior_Card_property.id]).gameObject.SetActive(false);
            line_dic.Remove(soldior_card.soldior_Card_property.id);
        }
        else if (support_num < support_num_limit && soldior_card.support_num < soldior_card.support_num_limit)
        {

            move_card.stop_move();

            if (supporter_Card_property.specialType ==SpecialType.supporter_monk)
            {
                StartCoroutine("support_hpimprove_coroutine", soldior_card);
            }
            else if (supporter_Card_property.specialType==SpecialType.supporter_ap)
            {
                StartCoroutine("support_ap_coroutine", soldior_card);
            }
            //soldior_card.current_health
        }
    } // main support function

    void add_support(Soldior_Card soldior_card) {
        support_num++;
        soldior_card.support_num++;

        if (line_dic.Count == 0 || (line_dic.Count == 1 && line_dic.ContainsValue(1)))
        {
            print("0");
            line_dic.Add(soldior_card.soldior_Card_property.id, 0);

            transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            line_renderer = transform.GetChild(2).GetChild(0).GetComponent<LineRenderer>();
            line_script = transform.GetChild(2).GetChild(0).GetComponent<Line>();

            line_script.soldior = soldior_card.gameObject;
            line_script.supporter = gameObject;
        }
        else if (line_dic.Count == 1 && line_dic.ContainsValue(0))
        {
            print("1");
            line_dic.Add(soldior_card.soldior_Card_property.id, 1);

            transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
            line_renderer = transform.GetChild(2).GetChild(1).GetComponent<LineRenderer>();

            line_script = transform.GetChild(2).GetChild(1).GetComponent<Line>();
            line_script.soldior = soldior_card.gameObject;
            line_script.supporter = gameObject;
        }
    }   // add support to soldior
    void remove_support(Soldior_Card soldior_card)
    {
        soldior_card.support_num--;
        support_num--;
        if (line_dic.ContainsKey(soldior_card.soldior_Card_property.id))
        {
            transform.GetChild(2).GetChild(line_dic[soldior_card.soldior_Card_property.id]).gameObject.SetActive(false);
            line_dic.Remove(soldior_card.soldior_Card_property.id);
        }
    } // remove support from soldior

    IEnumerator support_hpimprove_coroutine(Soldior_Card soldior_card)
    {
        float dis = Vector2.Distance(transform.position, soldior_card.transform.position);
        if (dis <= supporter_Card_property.range)
        {
            add_support(soldior_card);            
            while (dis <= supporter_Card_property.range && line_dic.ContainsKey(soldior_card.soldior_Card_property.id))
            {
                dis = Vector2.Distance(transform.position, soldior_card.transform.position);
                improve_health(5f,soldior_card);
                
                yield return null;
            }            

            remove_support(soldior_card);

        }
    } // special support coroutine(monk)
    void improve_health(float improve_health, Soldior_Card soldior_card)
    {
        if (soldior_card.current_health < soldior_card.soldior_Card_property.health_base)
        {
            soldior_card.current_health += (improve_health * Time.deltaTime);
        }
        else
        {
            soldior_card.current_health = soldior_card.soldior_Card_property.health_base;
        }

    }

    IEnumerator support_ap_coroutine(Soldior_Card soldior_card)
    {
        float dis = Vector2.Distance(transform.position, soldior_card.transform.position);
        if (dis <= supporter_Card_property.range)
        {
            add_support(soldior_card);            
            soldior_card.soldior_Card_property.attack_point += 10;
            
            while (dis <= supporter_Card_property.range && line_dic.ContainsKey(soldior_card.soldior_Card_property.id))
            {
                dis = Vector2.Distance(transform.position, soldior_card.transform.position);                
                yield return null;
            }
            soldior_card.soldior_Card_property.attack_point -= 10;
            

            remove_support(soldior_card);

        }
    }       // special support coroutine(ap increase)
}

public enum SpecialType
{
    supporter_hp = 0, supporter_monk, supporter_ap
}



