using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class pathFollower : MonoBehaviour {

	List<Vector2> path = new List<Vector2>();
	void Start () 
	{
		path = mapMacker.Instance.GetPath (2,4,false);
		print (path.Count);
	}
	bool finish=false;
	int pathindex;
	void Update ()
	{	
		if (!finish) {
			Vector3 target = new Vector3 (path [pathindex].x, path [pathindex].y, transform.position.z);
			float dist = Vector3.Distance (target, transform.position);

			transform.position = Vector3.Slerp (transform.position, target, Time.deltaTime * 5);
			if (dist < 0.1f) {	
				if (pathindex < path.Count-1)
					pathindex++;
				else
					finish = true;

			}
		}
	}
}
