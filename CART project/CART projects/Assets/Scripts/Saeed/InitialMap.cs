using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;

public class InitialMap : MonoBehaviour {

	public static InitialMap Instance { get; private set;}
	private NodePreview[,] AllNodePreviews;
	public GameObject[] allCharsPrefabs;
	public GameObject tile;
	public int columnCount;
	public int rowCount;
	public float offset;
	public Vector3 StartPos;

	private GameObject SelectedOBJ;
	public Vector3 tileScale;
	public GameObject DeletBTN;

	public Color freeNodeColor;
	public Color blockNodeColor;
	public Color selectedNodeColor;


	public JSONNode levelJson { private set; get;}
	public int levelSupportCount { private set; get;}
	public int LevelSoldeirCount { private set; get;}

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{	
		levelJson = LocalDB.JsonForThisLevel (LocalDB.currentLevel);
		levelSupportCount = levelJson ["supportCount"].AsInt;
		LevelSoldeirCount = levelJson ["soldeirCount"].AsInt;
		CreateMap ();
	}

	public void CreateMap() 
	{	
		AllNodePreviews= new NodePreview[columnCount,rowCount];
		for(int i=0; i<columnCount ;i++)
		{
			for(int j=0;j<rowCount;j++)
			{	
				Vector3 pos = new Vector3((i * offset) + StartPos.x, StartPos.y-(j * offset),StartPos.z);
				GameObject newTile = Instantiate (tile,pos,tile.transform.rotation)as GameObject;
				newTile.transform.SetParent (transform);
				newTile.transform.localScale = tileScale;
				AllNodePreviews [i, j] = newTile.GetComponent<NodePreview> ();
				AllNodePreviews [i, j].SetNodeInfo (new Vector2(pos.x,pos.y),j,i,-2);
				if(i>=15)
					AllNodePreviews [i, j].SetColor (freeNodeColor);
				else
					AllNodePreviews [i, j].SetColor (blockNodeColor);
			}
		}
	}


	private NodePreview ClickedNode;

	public void tileClicked(NodePreview NP)
	{
		if (NP.column < 15)
			return;

		if (ClickedNode != null) {
			ClickedNode.SetColor (freeNodeColor);
			ClickedNode = null;
			return;
		}
		
	
		if (NP.usageID != -2)
			return;

		ClickedNode = NP;
		AllNodePreviews [ClickedNode.column, ClickedNode.row].SetColor (selectedNodeColor);
	}

	public void SetThisCartAsSelected(int index)
	{
		if (ClickedNode == null)
			return;
		CreatThisCart (index);
	}

	List<DefaultCard> SelectedNPList = new List<DefaultCard>();
	private void CreatThisCart(int index)
	{	
		Vector3 pos = new Vector3 (ClickedNode.position.x,ClickedNode.position.y,10);
		GameObject NewOBj =  Instantiate (allCharsPrefabs[index] , pos , allCharsPrefabs[index] .transform.rotation) as GameObject;
		DefaultCard defaultcart = NewOBj.GetComponent<DefaultCard> ();
		defaultcart.SetInfo (ClickedNode.row,ClickedNode.column);
		ClickedNode.SetUsageID (index); 
		SelectedNPList.Add (defaultcart);
		ClickedNode = null;
	}

	public void SceneCardSelected (GameObject card)
	{
		DeletBTN.SetActive (true);
		SelectedOBJ = card;
		Vector3 pos = SelectedOBJ.transform.position;
		pos.y += 0.5f;
		pos.x -= 0.4f;
		pos.z -= 1;
		DeletBTN.transform.position = pos;
	}

	public void DeleteSceneCart()
	{
		if (SelectedOBJ == null)
			return;
		DefaultCard defaultCard_ = SelectedOBJ.GetComponent<DefaultCard> ();
		int row = defaultCard_.row;
		int column = defaultCard_.column;
		AllNodePreviews [column, row].SetColor (freeNodeColor);
		AllNodePreviews [column, row].SetUsageID (-2);
		Destroy (SelectedOBJ);
		DeletBTN.SetActive (false);
	}

	void Update(){
		if(Input.GetKeyDown("h"))
		{
			MakeDefaultJsonForCards ();
		}
	}

	public void MakeDefaultJsonForCards()
	{	
		List<NodePreview> NPList = new List<NodePreview> ();
		JSONClass Node = new JSONClass();
		for (int i = 0; i < SelectedNPList.Count ; i++) 
		{
			JSONClass M = new JSONClass ();
			M ["c"].AsInt = SelectedNPList[i].column;
			M ["r"].AsInt = SelectedNPList[i].row;
			M ["id"].AsInt = SelectedNPList[i].cardID;

			if (SelectedNPList [i].type == enumCollection.cardType.soldeir)
				M ["type"] = "soldeir";
			else if(SelectedNPList[i].type==enumCollection.cardType.support)
				M ["type"] = "support";

			Node ["defaultCards"].Add (M);
		}
		if (Node != null) 
		{
			LocalDB.playerCards = Node.ToString ();
			print (Node.ToString ());
		}	
	}
}
