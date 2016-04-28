using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//####################################################################
//##### Use This Class To Show Node Info On Test Node Objects ########
//####################################################################

public class NodePreview : MonoBehaviour {

	public SpriteRenderer spriteRenderer;
	public int row;
	public int column;
	public Vector2 position;
	public int usageID;
    
	void Awake()
	{
		spriteRenderer.color = Color.white;
        
	}

	public void SetNodeInfo(Vector2 pos , int r,int c , int useID)
	{
		row = r;
		column = c;
		position = pos ;
		usageID = useID;
	}

    public void SetUsageID(int useID) 
    {
        usageID = useID;
    }

	public void SetColor(Color color)
	{
		spriteRenderer.color = color;
	}

	void OnMouseDown()
	{
        if (mapMacker.Instance!=null)
		    mapMacker.Instance .NodeClicked(this,false);
        

	}

	void OnMouseOver()
	{
		if(Input.GetMouseButtonDown(1))
		{
			mapMacker.Instance .NodeClicked(this,true);
		}
	}
    void Update() { 
        if(usageID!=-2 && usageID!=-1){
            spriteRenderer.color=Color.black;
        }
        
    }


}
