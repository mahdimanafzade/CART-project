﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// saied rajab
//###########################################
//##### use this class to macke map  ########
//###########################################

public class mapMacker : MonoBehaviour {

	public static mapMacker Instance{ private set; get;}
	public List<Vector3> defaultNodes;
    public List<Vector2> flag_Nodes;


	private NodePreview[,] TestNodeObjects;

	public Transform tilsParent;
	public GameObject NodeTile;
	public Vector3 tilsScale;
	public Vector2 mapCorner_leftUp;
	public float nodesOffset;
	public int columnsCount;
	public int rowsCount;
	private pathFinder newmap;
	private pathFinder.node[,] madeMapNodes;

    public Color redColor;
    public Color whiteColor;
    public Color blackColor;

	void Awake()
	{
		Instance = this;
		CreateMap ();
	}

	public void CreateMap() 
	{	
		newmap = new pathFinder(mapCorner_leftUp, columnsCount, rowsCount, nodesOffset,pathFinder.CollisionDetectionMode.medium);
		newmap.SetDefaultNodes(defaultNodes);        
		madeMapNodes = newmap.GetMapNodes();
		MackeMapTestShape(madeMapNodes);
		UpdateTestNodeState ();
	}

    public pathFinder.node GetNode(int id)
	{
        return newmap.GetNode(id);
    }
	public List<Vector2> GetPath(int startID, Vector2 EndPoint , bool avoidContact)
	{   
		newmap.GetPath(startID, EndPoint,avoidContact);
		UpdateTestNodeState ();
		return newmap.PathPoses ();
	}

	public List<pathFinder.node> GetPath(Vector2 startPoint, Vector2 EndPoint, int id,bool avoidContact)
	{   
		newmap.GetPath(startPoint, EndPoint,id,avoidContact);
		UpdateTestNodeState ();
		return newmap.PathNodes ();
	}

	public List<Vector2> GetPath(int startID, int EndPoint , bool avoidContact)
	{   
		newmap.GetPath(startID,EndPoint,avoidContact);
		UpdateTestNodeState ();
		return newmap.PathPoses ();
	}

	//############################################################
	//##### use this two methode for get range acoording step ####
	//############################################################

	public List<pathFinder.node> GetRange(int id , int step)
	{   
		List<pathFinder.node> InRangeTiles =  newmap.GetRange(id,step);
		ChangeTileRange (InRangeTiles);
		return InRangeTiles;
	}

	public List<pathFinder.node> GetRange(Vector2 pos , int step)
	{   
		List<pathFinder.node> InRangeTiles =  newmap.GetRange(pos,step);
		ChangeTileRange (InRangeTiles);
		return InRangeTiles;
	}


	private void ChangeTileRange(List<pathFinder.node> inRangeNide)
	{	
		pathFinder.node[,] allMapNodes = newmap.GetMapNodes();
		for (int i = 0; i < allMapNodes.GetLength (0); i++) 
		{	
			for (int j = 0; j < allMapNodes.GetLength (1); j++) 
			{
				if (inRangeNide.Contains (allMapNodes [i, j])) {
					TestNodeObjects [i, j].SetColor (Color.blue);
				} else {
					TestNodeObjects [i, j].SetColor (Color.white);
				}
			}
		}
	}


	public void MackeMapTestShape(pathFinder.node[,] allMapNodes)
	{	
		TestNodeObjects = new NodePreview[rowsCount,columnsCount];
		for(int i=0;i<allMapNodes.GetLength(0);i++)
		{	
			for(int j=0;j<allMapNodes.GetLength(1);j++)
			{
				Vector3 pos= new Vector3();
				pos.x = allMapNodes[i,j].pos.x;
				pos.y = allMapNodes[i,j].pos.y;
				GameObject newNode = Instantiate (NodeTile , pos , NodeTile.transform.rotation)as GameObject ;
				newNode.transform.SetParent (tilsParent);
				newNode.transform.localPosition = new Vector3(newNode.transform.position.x, newNode.transform.position.y, 1);
				newNode.transform.localScale = tilsScale;
				TestNodeObjects [i, j] = newNode.GetComponent<NodePreview> ();
			}
		}
	}

	//############################################################################################
	//#####################   Invoke this methode after any change in nodes State   ##############
	//############################################################################################

	private void UpdateTestNodeState()
	{	
		pathFinder.node[,] allMapNodes = newmap.GetMapNodes();
		List<pathFinder.node> foundPathNode = newmap.PathNodes ();
		for (int i = 0; i < allMapNodes.GetLength (0); i++) 
		{	
			for (int j = 0; j < allMapNodes.GetLength (1); j++) 
			{
				if (foundPathNode.Contains (allMapNodes [i, j])) 
				{
					TestNodeObjects [i, j].SetNodeInfo (allMapNodes [i, j].pos, allMapNodes [i, j].rowIndex, allMapNodes [i, j].columnIndex, allMapNodes [i, j].usageID);
                    TestNodeObjects[i, j].SetColor(redColor);
				} 
				else if (allMapNodes [i, j].usageID==-1) 
				{
					TestNodeObjects [i, j].SetNodeInfo (allMapNodes [i, j].pos, allMapNodes [i, j].rowIndex, allMapNodes [i, j].columnIndex,allMapNodes [i, j].usageID);
                    TestNodeObjects[i, j].SetColor(blackColor);
				}
				else 
				{
					TestNodeObjects [i, j].SetNodeInfo (allMapNodes[i,j].pos,allMapNodes[i,j].rowIndex,allMapNodes[i,j].columnIndex,allMapNodes [i, j].usageID);
                    TestNodeObjects[i, j].SetColor(whiteColor);
				}
			}
		}
	}


	//###############################################
	//############   For Test  ######################
	//###############################################

	private NodePreview NodePreview_1;
	private NodePreview NodePreview_2;

	public void NodeClicked(NodePreview NP , bool unwalkable)
	{	
		pathFinder.node[,] allMapNodes = newmap.GetMapNodes();
		if(unwalkable)
		{	
			if (allMapNodes [NP.row, NP.column].usageID!=-1)
				allMapNodes [NP.row, NP.column].usageID = -1;
			else
				allMapNodes [NP.row, NP.column].usageID = 0;

			UpdateTestNodeState ();
			return;
		}

		if(NodePreview_1==null)
		{
			NodePreview_1 = NP;
			return;
		}

		NodePreview_2 = NP;
		//GetPath (new Vector2(NodePreview_1.transform.position.x,NodePreview_1.transform.position.y) , new Vector2(NodePreview_2.transform.position.x,NodePreview_2.transform.position.y) ,true);
		NodePreview_1=null;
		NodePreview_2 = null;
	}

}
