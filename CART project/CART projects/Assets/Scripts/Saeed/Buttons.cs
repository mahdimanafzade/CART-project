using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {

	public int MyIndex;

	public enum ButtonType
	{	
		none,
		cartListBtns,
		playBtn,
		pauseBtn,
		deleteSceneCart
	}
	public ButtonType myType;

	public void IClicked()
	{
		switch (myType) 
		{
		case ButtonType.cartListBtns:
			InitialMap.Instance.SetThisCartAsSelected (MyIndex);
			break;
		case ButtonType.playBtn :
			break;
		case ButtonType.pauseBtn :
			break;
		case ButtonType.deleteSceneCart:
			InitialMap.Instance.DeleteSceneCart ();
			break;
		}
	}
}
