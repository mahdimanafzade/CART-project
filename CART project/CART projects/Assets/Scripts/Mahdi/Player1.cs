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
    static int selected_card_id = -1;
    int prev_selected_card_id = -1;

    Soldior_Card my_soldiorCard;
    Supporter_Card my_supporterCard;
    GameObject mycard_obj;


    GameObject prev_mycard_obj = null;

    Soldior_Card soldior_EnemyCard;
    Supporter_Card supporter_EnemyCard;



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
                else if (touch.phase == TouchPhase.Ended && finger_id == touch.fingerId && begin_selectedObj==raycasthit.collider.gameObject)
                {

                    if (raycasthit.collider.tag == "soldior_card" || raycasthit.collider.tag == "supporter_card")
                    {

                        mycard_obj = raycasthit.collider.gameObject;

                        if (prev_mycard_obj != null && prev_mycard_obj != mycard_obj) // clean selection prev card
                        {
                            if (prev_mycard_obj.tag == "soldior_card")
                            {
                                prev_mycard_obj.GetComponent<Soldior_Card>().fx_active.SetActive(false);
                                prev_mycard_obj.GetComponent<LineRenderer>().enabled = false;
                                prev_mycard_obj.GetComponent<Soldior_Card>().selected_card = false;
                            }
                            else if (prev_mycard_obj.tag == "supporter_card")
                            {
                                prev_mycard_obj.GetComponent<Supporter_Card>().fx_active.SetActive(false);
                                prev_mycard_obj.GetComponent<LineRenderer>().enabled = false;
                                prev_mycard_obj.GetComponent<Supporter_Card>().selected_card = false;
                            }
                        }

                        if (raycasthit.collider.tag == "soldior_card") // support
                        {
                            my_soldiorCard = mycard_obj.GetComponent<Soldior_Card>();
                            my_soldiorCard.selected_card = !my_soldiorCard.selected_card;
                            if (my_soldiorCard.selected_card)
                            {
                                print("active");
                                selected_card_id = my_soldiorCard.soldior_Card_property.id;
                                my_soldiorCard.fx_active.SetActive(true);
                                my_soldiorCard.GetComponent<Create_circle>().create_circle();
                                mycard_obj.GetComponent<LineRenderer>().enabled = true;

                                
                                if (prev_mycard_obj != mycard_obj && prev_mycard_obj != null && prev_mycard_obj.tag == "supporter_card" )
                                {

                                    print("support");
                                    prev_mycard_obj.GetComponent<Supporter_Card>().support(my_soldiorCard);
                                }
                            }
                            else
                            {
                                print("dis_active");
                                my_soldiorCard.fx_active.SetActive(false);
                                mycard_obj.GetComponent<LineRenderer>().enabled = false;
                                selected_card_id = -1;
                            }
                        }
                        else if (raycasthit.collider.tag == "supporter_card")
                        {
                            my_supporterCard = mycard_obj.GetComponent<Supporter_Card>();
                            my_supporterCard.selected_card = !my_supporterCard.selected_card;
                            if (my_supporterCard.selected_card)
                            {
                                print("active");
                                selected_card_id = my_supporterCard.supporter_Card_property.id;
                                my_supporterCard.fx_active.SetActive(true);

                                my_supporterCard.GetComponent<Create_circle>().create_circle();
                                mycard_obj.GetComponent<LineRenderer>().enabled = true;

                            }
                            else
                            {
                                print("dis_active");
                                my_supporterCard.fx_active.SetActive(false);
                                mycard_obj.GetComponent<LineRenderer>().enabled = false;
                                selected_card_id = -1;
                            }
                        }
                        prev_mycard_obj = mycard_obj;
                    }
                    else if (raycasthit.collider.tag == "soldior_enemy") // attack
                    {                        
                        if (prev_mycard_obj.tag == "soldior_card")
                        {
                            soldior_EnemyCard = raycasthit.collider.gameObject.GetComponent<Soldior_Card>();
                            if (selected_card_id != -1)
                            {
                                selected_card_id = -1;
                                my_soldiorCard.selected_card = false;
                                my_soldiorCard.fx_active.SetActive(false);
                                mycard_obj.GetComponent<LineRenderer>().enabled = false;
                                print("attack");
                                my_soldiorCard.attack(soldior_EnemyCard);

                            }

                        }
                    }
                    else if (raycasthit.collider.tag == "supporter_enemy") // attack
                    {
                        if (prev_mycard_obj.tag == "soldior_card")
                        {
                            supporter_EnemyCard = raycasthit.collider.gameObject.GetComponent<Supporter_Card>();
                            if (selected_card_id != -1)
                            {
                                selected_card_id = -1;
                                my_soldiorCard.selected_card = false;
                                my_soldiorCard.fx_active.SetActive(false);
                                mycard_obj.GetComponent<LineRenderer>().enabled = false;
                                print("attack");
                                my_soldiorCard.attack(supporter_EnemyCard);

                            }

                        }
                    }
                    else  //move
                    {
                        print("ss");
                        if (selected_card_id != -1)
                        {
                            selected_card_id = -1;
                            prev_mycard_obj = null;

                            if (mycard_obj.tag == "soldior_card")
                            {
                                my_soldiorCard.selected_card = false;
                                my_soldiorCard.fx_active.SetActive(false);
                                mycard_obj.GetComponent<LineRenderer>().enabled = false;
                                
                                my_soldiorCard.move(new Vector2(ray.origin.x, ray.origin.y));
                            }
                            else if (mycard_obj.tag == "supporter_card")
                            {
                                my_supporterCard.selected_card = false;
                                my_supporterCard.fx_active.SetActive(false);
                                mycard_obj.GetComponent<LineRenderer>().enabled = false;
                                my_supporterCard.move(new Vector2(ray.origin.x, ray.origin.y));
                            }
                        }

                    }
                }
            }


        }
    }
}
