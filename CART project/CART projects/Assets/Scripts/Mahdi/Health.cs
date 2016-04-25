using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour {

    public bool death_active = false;

    public Image green_health;
    public float current_health;
    public float health_base = 100f;
    public Card card;
	// Use this for initialization
	void Start () {
	    card=GetComponent<Card>();
        current_health = health_base;
        green_health = transform.FindChild("red").FindChild("green").gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        display_health();
        if (card.card_state == Card_state.death && !death_active)
        {
            Destroy(gameObject, 0.2f);
            
            GameManager._instance.remove_FromGameManager(gameObject);
            death_active = true;
        }
    }
    
    void display_health()
    {
        green_health.fillAmount = (float)current_health / (float)health_base;
    }
}
