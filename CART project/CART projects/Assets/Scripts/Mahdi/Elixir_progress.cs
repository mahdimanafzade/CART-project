using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Elixir_progress : MonoBehaviour {
    public float fill_time=15f;// time to fill completely
    float increase_val;// increase value per second
    Image elixir_progressImg;
    Image elixir_front;
    public float scale_factor=10f; // 10 parts
    
	// Use this for initialization
	void Start () {
        
        increase_val = 1f / fill_time;

        elixir_progressImg = GetComponent<Image>();
        elixir_front = transform.GetChild(0).GetComponent<Image>();
        StartCoroutine("elixir_increase");
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public bool check_elixir(int dis_value)
    {
        if (dis_value <= elixir_progressImg.fillAmount * scale_factor)
        {
            elixir_progressImg.fillAmount -= dis_value / scale_factor;
            elixir_front.fillAmount = Mathf.Floor(elixir_progressImg.fillAmount * scale_factor) / scale_factor;
            return true;
        }
        return false;

    }
    IEnumerator elixir_increase() { 
        while(true){
            if(elixir_progressImg.fillAmount<1){
                elixir_progressImg.fillAmount+=Time.deltaTime*increase_val;
                elixir_front.fillAmount = Mathf.Floor(elixir_progressImg.fillAmount * scale_factor) / scale_factor;
                
            }
            else{
                elixir_progressImg.fillAmount=1;
                elixir_front.fillAmount=1;
            }
            
            
            yield return null;
        }
    }
}
