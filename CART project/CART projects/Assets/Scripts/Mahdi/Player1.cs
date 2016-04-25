// Developer:Mahdi Manafzade
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Player1 : MonoBehaviour
{
    GameObject begin_selectedObj;
    public Ray ray;
    int finger_id = -1;
    RaycastHit2D raycasthit;
    Touch touch;
    int selected_card_id = -1;



    GameObject mycard_obj;
    GameObject prev_mycard_obj = null;

    Card enemyCard;
    Health health_enemyCard;

    Card myCard;



    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            ray = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0f));
            //print(ray.origin);
            raycasthit = Physics2D.GetRayIntersection(ray);

            if (raycasthit != null && raycasthit.collider != null)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    begin_selectedObj = raycasthit.collider.gameObject;
                    finger_id = touch.fingerId;
                }
                else if (touch.phase == TouchPhase.Ended && finger_id == touch.fingerId && begin_selectedObj == raycasthit.collider.gameObject)
                {

                    if (raycasthit.collider.tag == "soldior_card" || raycasthit.collider.tag == "supporter_card")
                    {

                        mycard_obj = raycasthit.collider.gameObject;

                        if (prev_mycard_obj != null && prev_mycard_obj != mycard_obj) // clean selection prev card
                        {
                            if (prev_mycard_obj.tag == "soldior_card" || prev_mycard_obj.tag == "supporter_card")
                            {
                                prev_mycard_obj.GetComponent<Card>().fx_active.SetActive(false);
                                prev_mycard_obj.GetComponent<LineRenderer>().enabled = false;
                                prev_mycard_obj.GetComponent<Card>().selected_card = false;
                            }
                        }
                        myCard = mycard_obj.GetComponent<Card>();
                        myCard.selected_card = !myCard.selected_card;
                        if (myCard.selected_card)
                        {
                            print("active");
                            selected_card_id = myCard.id;
                            myCard.fx_active.SetActive(true);
                            mycard_obj.GetComponent<Create_circle>().create_circle();
                            mycard_obj.GetComponent<LineRenderer>().enabled = true;
                            if (raycasthit.collider.tag == "soldior_card")//support
                            {
                                if (prev_mycard_obj != mycard_obj && prev_mycard_obj != null && prev_mycard_obj.tag == "supporter_card")
                                {
                                    print("support");
                                    prev_mycard_obj.GetComponent<Support_card>().support(mycard_obj);
                                }
                            }
                        }
                        else
                        {
                            print("dis_active");
                            myCard.fx_active.SetActive(false);
                            mycard_obj.GetComponent<LineRenderer>().enabled = false;
                            selected_card_id = -1;
                        }

                        prev_mycard_obj = mycard_obj;
                    }
                    else if (prev_mycard_obj!=null && raycasthit.collider.tag == "soldior_enemy" || raycasthit.collider.tag == "supporter_enemy") // attack
                    {
                        if (prev_mycard_obj.tag == "soldior_card")
                        {
                            health_enemyCard = raycasthit.collider.gameObject.GetComponent<Health>();
                            if (selected_card_id != -1)
                            {
                                selected_card_id = -1;
                                myCard.selected_card = false;
                                myCard.fx_active.SetActive(false);
                                mycard_obj.GetComponent<LineRenderer>().enabled = false;
                                print("attack");
                                mycard_obj.GetComponent<Attack_card>().attack(health_enemyCard);

                            }

                        }
                    }

                    else  //move
                    {
                        if (selected_card_id != -1)
                        {
                            selected_card_id = -1;
                            //prev_mycard_obj = null;

                            if (mycard_obj.tag == "soldior_card" || mycard_obj.tag == "supporter_card")
                            {
                                myCard.selected_card = false;
                                myCard.fx_active.SetActive(false);
                                mycard_obj.GetComponent<LineRenderer>().enabled = false;

                                mycard_obj.GetComponent<Move_card>().move(new Vector2(ray.origin.x, ray.origin.y));
                            }
                        }

                    }
                }
            }


        }
    }
}
