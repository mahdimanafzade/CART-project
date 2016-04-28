using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class SimpleJSONBuilder  
{
	private class ThisJsonData
	{
		public string head;
		public object[] body;
		public ThisJsonData(string head , object[] body)
		{
			this.head=head;
			this.body=body;
		}
	}
	private List<ThisJsonData> JsonList ;

	public SimpleJSONBuilder()
	{
		JsonList = new List<ThisJsonData> ();
	}

	public void Reset()
	{
		JsonList = new List<ThisJsonData> ();
	}

	public void AddField(string head , object[] body)
	{	
		ThisJsonData node = new ThisJsonData (head,body);
		JsonList.Add (node);
	}

	public string Build()
	{	
		JSONNode N = new JSONClass();
		foreach(ThisJsonData JLN in JsonList)
		{	
			if(JLN.body.Length==1)
			{
				N.Add(JLN.head,  CreateJsonDate(JLN.body[0]));
			}
			else
			{	
				JSONArray listNode = new JSONArray();
				for(int j=0;j<JLN.body.Length;j++)
				{	
					listNode.Add(CreateJsonDate(JLN.body[j]));
				}
				N.Add(JLN.head,  listNode);
			}
		}
		return(N.ToString());
	}

	private JSONData CreateJsonDate(object JLN)
	{	
		if (JLN.GetType () == typeof(string)) {
			return new JSONData (JLN.ToString ());
		} else if (JLN.GetType () == typeof(float)) {
			return new JSONData (float .Parse (JLN.ToString ()));
		} else if (JLN.GetType () == typeof(int)) {	
			return new JSONData (int.Parse(JLN.ToString()));
		} else if (JLN.GetType () == typeof(double)) {
			return new JSONData (double .Parse (JLN.ToString ()));
		} else {
			throw new UnityException("wronge type");
		}
	}
}

