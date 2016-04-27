using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum Town_state { 
    none,death
}
public class Town : MonoBehaviour {
    public bool death_active = false;

    public Image green_health;
    public float health_base;
    public float current_health;
    public Town_state town_state;
	// Use this for initialization
	void Start () {
        green_health = transform.FindChild("red").FindChild("green").gameObject.GetComponent<Image>();
        town_state = Town_state.none;
        current_health = health_base;
	}

    // Update is called once per frame
    void Update()
    {
        display_health();
        if (town_state == Town_state.death && !death_active)
        {
            Destroy(gameObject, 0.2f);

            //GameManager._instance.remove_FromGameManager(gameObject);
            death_active = true;
        }
    }

    void display_health()
    {
        green_health.fillAmount = (float)current_health / (float)health_base;
    }

}
