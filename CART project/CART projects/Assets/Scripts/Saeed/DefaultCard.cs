using UnityEngine;
using System.Collections;

public class DefaultCard : MonoBehaviour {

	public enumCollection.cardType type = new enumCollection.cardType(); 

	public int row{ private set; get;}
	public int column { private set; get; }

	public int cardID;

	void OnMouseDown(){
		InitialMap.Instance.SceneCardSelected (gameObject);
	}

	public void SetInfo(int r,int c)
	{
		row = r;
		column = c;
	}


}
