//Developer: Mahdi manafzade
using UnityEngine;
using System.Collections;

public class Create_circle : MonoBehaviour {
    public LineRenderer circle_renderer;
    int range;
	// Use this for initialization
	void Start () {
        circle_renderer = GetComponent<LineRenderer>();
        if (gameObject.tag == "soldior_card")
        {
            range = GetComponent<Attack_card>().range;
        }
        else if (gameObject.tag == "supporter_card")
        {
            range = GetComponent<Support_card>().range;
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void create_circle()
    {
        float x, y;

        float theta_scale = 0.1f;
        int size = (int)(Mathf.Ceil((2f * Mathf.PI) / theta_scale)) + 1;

        circle_renderer.SetColors(Color.cyan, Color.cyan);
        circle_renderer.SetWidth(0.5F, 0.5F);
        circle_renderer.SetVertexCount(size);

        int i = 0;
        for (float theta = 0; theta <= 2 * Mathf.PI + 0.1f; theta += 0.1f, i++)
        {
            x = range * Mathf.Cos(theta);
            y = range * Mathf.Sin(theta);

            Vector3 pos = transform.position + new Vector3(x, y, 0);

            circle_renderer.SetPosition(i, pos);

        }
    }

}
