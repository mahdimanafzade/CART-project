using UnityEngine;
using System.Collections;

public class RayCastFromCamera : MonoBehaviour {

	public Camera Camera_;
	public Vector2 HitPos{ private set; get;}
	public LayerMask RayCastMask;
	void Update () 
	{
		if(Input.GetMouseButton(0))
		{

			Vector2 rayPos = new Vector2(Camera_.ScreenToWorldPoint(Input.mousePosition).x, Camera_.ScreenToWorldPoint(Input.mousePosition).y);
			RaycastHit2D hit=Physics2D.Raycast(rayPos, Vector2.zero, 1000f,RayCastMask);
		
			if (hit) {
				if (hit.collider.CompareTag ("mapTile")) {
					print (hit.collider.gameObject.name);
				}
				else if (hit.collider.CompareTag ("card")) {
					print ("card");
				}
			}
		}
			
	}
}
