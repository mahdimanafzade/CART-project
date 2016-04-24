// Developer:Mahdi Manafzade

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



public class Soldior_Card : MonoBehaviour
{
    public Card card;
    Move_card move_card;
    public Soldior_Card_property soldior_Card_property;

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
        move_card = GetComponent<Move_card>();
        current_health = soldior_Card_property.health_base;
        card.card_state = Card_state.none;

        fx_active = transform.GetChild(0).gameObject;
        green_health = transform.FindChild("red").FindChild("green").gameObject.GetComponent<Image>();

        
        
        
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
        green_health.fillAmount = (float)current_health / (float)soldior_Card_property.health_base;
    }


    public void attack(Soldior_Card card_enemy)
    {
            card.card_state = Card_state.attack;
            StopCoroutine("attack_coroutine");
            move_card.stop_move();

            print(card_enemy);
            StartCoroutine("attack_coroutine", card_enemy);
    }

    IEnumerator attack_coroutine(Soldior_Card card_enemy)
    {
        float disToEnemy;
        while (card.card_state == Card_state.attack)
        {
            disToEnemy = Vector2.Distance(transform.position, card_enemy.transform.position);

            if (disToEnemy < soldior_Card_property.range)
            {
                if (card_enemy.current_health > 0)
                {
                    card_enemy.current_health -= Time.deltaTime * soldior_Card_property.attack_point * soldior_Card_property.attack_ratio;
                }
                else
                {
                    card_enemy.card.card_state = Card_state.death;
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
    public void move(Vector2 target)
    {
        card.card_state = Card_state.move;
        StopCoroutine("attack");
        move_card.move_mode(soldior_Card_property.id,target, ref soldior_Card_property.speed);

    }

    public void attack(Supporter_Card card_enemy)
    {
        card.card_state = Card_state.attack;
        StopCoroutine("attack_coroutine");
        move_card.stop_move();
        StartCoroutine("attack_coroutine", card_enemy);
    }

    IEnumerator attack_coroutine(Supporter_Card card_enemy)
    {
        float disToEnemy;
        while (card.card_state == Card_state.attack)
        {
            disToEnemy = Vector2.Distance(transform.position, card_enemy.transform.position);

            if (disToEnemy < soldior_Card_property.range)
            {
                if (card_enemy.current_health > 0)
                {
                    card_enemy.current_health -= Time.deltaTime * soldior_Card_property.attack_point * soldior_Card_property.attack_ratio;
                }
                else
                {
                    card_enemy.card.card_state = Card_state.death;
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

  



    
}






