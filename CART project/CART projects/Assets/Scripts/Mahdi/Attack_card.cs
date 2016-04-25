// Developer:Mahdi Manafzade

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



public class Attack_card : MonoBehaviour
{
    public GameObject closest_card = null;
    public float min_dis = Mathf.Infinity;

    public int range = 2;
    public int attack_point = 5;
    public int attack_ratio = 4;

    public Card card;
    Move_card move_card;
    Health health_component;
   // public Soldior_Card_property soldior_Card_property;

    LineRenderer line_renderer;
    Line line_script;    
    public int support_num = 0;
    public int support_num_limit = 2;




    void find_closestCard(out GameObject closest_card, out float min_dis)
    {
        closest_card = null;
        min_dis = Mathf.Infinity;

        int i;
        float dis;


        if (gameObject.tag == "soldior_enemy")
        {
            for (i = 0; i < GameManager._instance.soldior_cards.Count; i++)
            {
                dis = Vector3.Distance(transform.position, GameManager._instance.soldior_cards[i].transform.position);
                if (dis < min_dis)
                {
                    min_dis = dis;
                    closest_card = GameManager._instance.soldior_cards[i];
                }
            }
            for (i = 0; i < GameManager._instance.supporter_cards.Count; i++)
            {

                dis = Vector3.Distance(transform.position, GameManager._instance.supporter_cards[i].transform.position);
                if (dis < min_dis)
                {
                    min_dis = dis;
                    closest_card = GameManager._instance.supporter_cards[i];
                }
            }
        }
        else if (gameObject.tag == "soldior_card")
        {
            for (i = 0; i < GameManager._instance.soldior_enemies.Count; i++)
            {
                dis = Vector3.Distance(transform.position, GameManager._instance.soldior_enemies[i].transform.position);
                if (dis < min_dis)
                {
                    min_dis = dis;
                    closest_card = GameManager._instance.soldior_enemies[i];
                }
            }
            for (i = 0; i < GameManager._instance.supporter_enemies.Count; i++)
            {

                dis = Vector3.Distance(transform.position, GameManager._instance.supporter_enemies[i].transform.position);
                if (dis < min_dis)
                {
                    min_dis = dis;
                    closest_card = GameManager._instance.supporter_enemies[i];
                }
            }
        }

    }

    // Use this for initialization
    void Start()
    {

        card=GetComponent<Card>();
        move_card = GetComponent<Move_card>();
        health_component = GetComponent<Health>();
        //current_health = soldior_Card_property.health_base;
        card.card_state = Card_state.none;

    }

    // Update is called once per frame
    void Update()
    {
        if (card.card_state != Card_state.death)
        {
            find_closestCard(out closest_card, out min_dis);
            if (closest_card != null)
            {
                if (card.card_state == Card_state.none && min_dis <= range)
                {
                    print("1111");
                    attack(closest_card.GetComponent<Health>());
                }
            }
        }
    }
        
    

    public void attack(Health health_cardEnemy)
    {
            card.card_state = Card_state.attack;
            StopCoroutine("attack_coroutine");
            move_card.stop_move();


            StartCoroutine("attack_coroutine", health_cardEnemy);
    }

    IEnumerator attack_coroutine(Health health_cardEnemy)
    {
        float disToEnemy;
        while (card.card_state == Card_state.attack)
        {
            disToEnemy = Vector2.Distance(transform.position, health_cardEnemy.transform.position);

            if (disToEnemy < range)
            {
                if (health_cardEnemy.current_health > 0)
                {
                    health_cardEnemy.current_health -= Time.deltaTime * attack_point * attack_ratio;
                }
                else
                {
                    print("8888");
                    health_cardEnemy.card.card_state = Card_state.death;
                    
                    card.card_state = Card_state.none;
                    break;
                }
                yield return null;
            }
            else
            {
                card.card_state = Card_state.none;
                break;
            }

        }
    }
    /*public void move(Vector2 target)
    {
        card.card_state = Card_state.move;
        StopCoroutine("attack");
        move_card.move(target);

    }*/
}






