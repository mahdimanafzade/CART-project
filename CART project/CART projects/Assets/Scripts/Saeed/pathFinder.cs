using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// saied rajab
//####################################
//##### A* 8-way pathfinding  ########
//####################################

public class pathFinder  {

	public enum CollisionDetectionMode
	{
		easy,
		medium,
		hard
	}

	[System.Serializable]
	public class node
	{	
		public int rowIndex;
		public int columnIndex;
		public Vector2 pos;
		public node parent;
		public int usageID;
		public int g;
		public int h;
		public int f;
	}

	private int startID;
	private int endID;
	private const int freeNodesID = -2;
	private node[,] NodeArray;

	private node currentPoint;
	private node StartPoint; 
	private node TargetPoint;

	private List<node> openList;
	private List<node> closList;

	private int map_width;
	private int map_height;
	private Vector2 map_leftUpCorner;
	private float map_offset;

	private CollisionDetectionMode CollisionMode = new pathFinder.CollisionDetectionMode();

	//#####################################################################################################
	//########                    pass map info to constructor                                    #########
	//#####################################################################################################

	public pathFinder(Vector2 leftUpCorner , int width , int height , float offset , CollisionDetectionMode CDM)
	{	
		CollisionMode = CDM;
		NodeArray = new node[height,width];
		map_leftUpCorner = leftUpCorner;
		map_width = width;
		map_height = height;
		map_offset = offset;
		CreateMap ();
		ResetForNewPath ();
	}

	//######################################
	//##### Set All Unwalkable List ########
	//######################################

	public void SetDefaultNodes(List<Vector3> defaultNodes)
	{
		foreach(Vector3 tmp in defaultNodes)
		{	
			NodeArray [(int)tmp.x, (int)tmp.y].usageID = (int)tmp.z;
		}
	}

	//##############################################
	//##### Add One Node To Unwalkable List ########
	//##############################################
	/*
	public void AddToUnwalkableList(List<Vector2> UnwalkList)
	{
		unwalkableList = UnwalkList;
		foreach(node tmp in NodeArray)
		{	
			if (unwalkableList.Contains (tmp.pos)) 
				tmp.usageID = -1;
		}
	}
*/
	//#############################################################################
	//##### Reset Variable Depended last found path for calculate new path ########
	//#############################################################################

	private void ResetForNewPath()
	{	
		StartPoint = new node ();
		TargetPoint = new node ();
		currentPoint = new node ();
		openList = new List<node> ();
		closList = new List<node> ();
		foreach(node tmp in NodeArray)
		{
			tmp.f = 0;
			tmp.g = 0;
			tmp.f = 0;
			tmp.parent = null;
		}
	}

	//###############################################################
	//##### Initilize map according recived width and height ########
	//###############################################################

	private void CreateMap()
	{
		for(int i=0; i<map_width ; i++)
		{
			for(int j=0;j<map_height ; j++)
			{
				node newNode = new node ();
                newNode.pos = new Vector2((i * map_offset) + map_leftUpCorner.x, map_leftUpCorner.y-(j * map_offset));
				newNode.usageID = freeNodesID;
				newNode.rowIndex = j;
				newNode.columnIndex = i;
				newNode.f = 0;
				newNode.g = 0;
				newNode.f = 0;
				newNode.parent = null;
				NodeArray [j,i] = newNode;
			}
		}
	}
		
	//####################################################################
	//##### Call this method to calculate pass betweeon two point ########
	//####################################################################

	public void GetPath(Vector2 from , Vector2 to , bool avoidContact)
	{	
		ResetForNewPath ();
		float distToStart = Vector2.Distance (NodeArray[0,0].pos,from);
		float distToEnd = Vector2.Distance (NodeArray[0,0].pos,to);
		foreach (node tmp in NodeArray) 
		{	
			float dist = Vector2.Distance (tmp.pos,from);
			if (dist < distToStart) {
				distToStart = dist;
				StartPoint = tmp;
			}
			float dist2 = Vector2.Distance (tmp.pos,to);
			if (dist2 < distToEnd) {
				distToEnd = dist2;
				TargetPoint = tmp;
			}
		}
		openList.Add (StartPoint);
		StartPathFinding (avoidContact);
	}

	//####################################################################
	//##### Call this method to calculate pass betweeon two point ########
	//####################################################################

	public void GetPath(int fromID , Vector2 to ,bool avoidContact)
	{	
		startID = fromID;
		ResetForNewPath ();
		float distToEnd = Vector2.Distance (NodeArray[0,0].pos,to);
		bool startFound = false;
		bool endFound = false;
		foreach (node tmp in NodeArray) 
		{	
			if (tmp.usageID == fromID) 
			{
				StartPoint = tmp;
				startFound = true;
			}
			float dist2 = Vector2.Distance (tmp.pos,to);
			if (dist2 < distToEnd) 
			{
				distToEnd = dist2;
				TargetPoint = tmp;
				endFound = true;
			}
		}
		if (!startFound || !endFound)
			return;
	
		openList.Add (StartPoint);
		StartPathFinding (avoidContact);
	}


	//####################################################################
	//##### Call this method to calculate pass betweeon two point ########
	//####################################################################

	public void GetPath(int fromID , int toID , bool avoidContact)
	{	
		ResetForNewPath ();
		bool startFound = false;
		bool endFound = false;
		foreach (node tmp in NodeArray) 
		{	
			if (tmp.usageID == fromID) 
			{
				StartPoint = tmp;
				startFound = true;
			}
			if (tmp.usageID == toID) 
			{
				TargetPoint = tmp;
				endFound = true;
			}
		}
		if (!startFound || !endFound)
			return;
		
		openList.Add (StartPoint);
		StartPathFinding (avoidContact);
	}

	//###########################################
	//##### get Rang Around This Node ID ########
	//###########################################


	public List<node> GetRange( int nodeID, int step)
	{	
		node node_ = new node();
		foreach (node tmp in NodeArray) 
		{	
			if (tmp.usageID == nodeID) 
			{
				node_ = tmp;
			}
		}
		List<node> neighbors = Find_All_Neighbors(node_);
		step--;
		while (step > 0) 
		{	
			List<node> lastNeighbors = new List<node> ();
			lastNeighbors.AddRange(neighbors);
			foreach (node tmp in lastNeighbors) 
			{
				List<node> newNeighbors = Find_All_Neighbors (tmp);
				foreach (node n in newNeighbors) 
				{
					if (!neighbors.Contains (n))
						neighbors.Add (n);
				}
			}
			step--;
		}

		return neighbors;
	}

	//###########################################
	//##### get Rang Around This Vector2 ########
	//###########################################

	public List<node> GetRange( Vector2 nodePos, int step)
	{	
		float distToEnd = Vector2.Distance (NodeArray[0,0].pos,nodePos);
		node node_ = new node();
		foreach (node tmp in NodeArray) 
		{	
			float dist = Vector2.Distance (tmp.pos,nodePos);
			if (dist < distToEnd) 
			{
				distToEnd = dist;
				node_ = tmp;
			}
		}

		List<node> neighbors = Find_All_Neighbors(node_);
		step--;
		while (step > 0) 
		{	
			List<node> lastNeighbors = new List<node> ();
			lastNeighbors.AddRange(neighbors);
			foreach (node tmp in lastNeighbors) 
			{
				List<node> newNeighbors = Find_All_Neighbors (tmp);
				foreach (node n in newNeighbors) 
				{
					if (!neighbors.Contains (n))
						neighbors.Add (n);
				}
			}
			step--;
		}

		return neighbors;
	}

	//#########################################
	//##### Start of path finding loop ########
	//#########################################

	private void StartPathFinding(bool AvoidContact)
	{
		node lowestF = FindLowestF ();
		currentPoint = lowestF;
		openList.Remove (currentPoint);
		closList.Add (currentPoint);


		if(AvoidContact)
			FindNeighbors_ContactInclude (currentPoint);
		else
			FindNeighbors_IgnoreContact (currentPoint);


		foreach(node tmp in closList)
		{
			if(tmp.pos == TargetPoint.pos)
			{	
				List<node> node_ = PathNodes();
				/*
				foreach(node tp in node_)
				{
					Debug.LogWarning ("row :" + tp.rowIndex + "column:"+tp.columnIndex + "usageid : "+tp.usageID);
				}
				*/


				node_[0].usageID=freeNodesID;
				node lastPathNode = node_[node_.Count-1];
				lastPathNode.usageID = startID;
				/*
				foreach(node tp in node_)
				{
					Debug.LogError ("row :" + tp.rowIndex + "column:"+tp.columnIndex + "usageid : "+tp.usageID);
				}
				*/



				return;
			}
		}

		if(openList.Count==0 && !closList.Contains(TargetPoint))
		{
			return;
		}

		StartPathFinding (AvoidContact);
	}


	//#################################################
	//##### return All 8 neighbors of current  ########
	//#################################################

	private List<node> Find_All_Neighbors(node node_)
	{
		int row = node_.rowIndex;
		int column = node_.columnIndex;
		List<node> Neighbors = new List<node> ();

		if (column - 1 >= 0) 
		{
			if (row - 1 >= 0) 
				Neighbors.Add (NodeArray [row - 1, column - 1]);
			
			Neighbors.Add (NodeArray [row , column - 1]);
			if (row + 1 < map_height)
				Neighbors.Add (NodeArray [row + 1, column - 1]);
		}

		if(row-1>=0)
			Neighbors.Add (NodeArray [row - 1, column]);
		

		if(row+1<map_height)
			Neighbors.Add (NodeArray [row + 1, column ]);
		

		if (column + 1 <map_width) 
		{
			if (row - 1 >= 0) 
				Neighbors.Add (NodeArray [row - 1, column + 1]);


			Neighbors.Add (NodeArray [row , column + 1]);


			if (row + 1 < map_height)
				Neighbors.Add (NodeArray [row + 1, column + 1]);
		}

		return Neighbors;
	}

	//#########################################################
	//##### 8 neighbors of current point Avoid Contact ########
	//#########################################################

	private void FindNeighbors_ContactInclude(node node_)
	{
		int row = node_.rowIndex;
		int column = node_.columnIndex;

		if (column - 1 >= 0) 
		{
			if (row - 1 >= 0) 
			{
				if (NodeArray [row - 1, column - 1].usageID==freeNodesID && !closList.Contains(NodeArray [row - 1, column - 1]) && CheckUnwalkableNeighbors(NodeArray [row - 1, column - 1])) {
					AddToOpenList (NodeArray [row - 1, column - 1]);
				}
			}

			if (NodeArray [row, column - 1].usageID==freeNodesID && !closList.Contains(NodeArray [row, column - 1]) && CheckUnwalkableNeighbors(NodeArray [row , column - 1])) 
			{
				AddToOpenList (NodeArray [row , column - 1]);
			}

			if (row + 1 < map_height)
			{
				if (NodeArray [row + 1, column - 1].usageID==freeNodesID && !closList.Contains(NodeArray [row + 1, column - 1]) && CheckUnwalkableNeighbors(NodeArray [row + 1, column - 1])) {
					AddToOpenList (NodeArray [row + 1, column - 1]);
				}
			}
		}

		if(row-1>=0)
		{
			if (NodeArray [row - 1, column].usageID==freeNodesID && !closList.Contains(NodeArray [row - 1, column]) && CheckUnwalkableNeighbors(NodeArray [row - 1, column])) 
			{
				AddToOpenList (NodeArray [row - 1, column]);
			}
		}

		if(row+1<map_height)
		{
			if (NodeArray [row + 1, column].usageID==freeNodesID && !closList.Contains(NodeArray [row + 1, column]) && CheckUnwalkableNeighbors(NodeArray [row + 1, column ])) 
			{
				AddToOpenList (NodeArray [row + 1, column ]);
			}
		}
			
		if (column + 1 <map_width) 
		{
			if (row - 1 >= 0) 
			{
				if (NodeArray [row - 1, column + 1].usageID==freeNodesID && !closList.Contains(NodeArray [row - 1, column + 1]) && CheckUnwalkableNeighbors(NodeArray [row - 1, column + 1])) {
					AddToOpenList (NodeArray [row - 1, column + 1]);
				}
			}

			if (NodeArray [row, column + 1].usageID==freeNodesID && !closList.Contains(NodeArray [row, column + 1]) && CheckUnwalkableNeighbors(NodeArray [row , column + 1])) 
			{
				AddToOpenList (NodeArray [row , column + 1]);
			}

			if (row + 1 < map_height)
			{
				if (NodeArray [row + 1, column + 1].usageID==freeNodesID && !closList.Contains(NodeArray [row + 1, column + 1]) && CheckUnwalkableNeighbors(NodeArray [row + 1, column + 1])) {
					AddToOpenList (NodeArray [row + 1, column + 1]);
				}
			}
		}
	}


	private bool CheckUnwalkableNeighbors(node CNode)
	{	

		if (CollisionMode == CollisionDetectionMode.easy)
			return true;

		int row = CNode.rowIndex;
		int column = CNode.columnIndex;

		bool upBlock = false;
		bool rightBlock = false;
		bool leftBlock = false;
		bool downBlock = false;

		if(row-1>=0)
			if (NodeArray [row - 1, column].usageID!=freeNodesID)
			upBlock = true;

		if(row+1<map_height)
			if (NodeArray [row + 1, column].usageID!=freeNodesID)
			downBlock = true;

		if(column+1<map_width)
			if(NodeArray [row, column+1].usageID!=freeNodesID)
			rightBlock = true;

		if(column-1>=0)
			if(NodeArray [row, column-1].usageID!=freeNodesID)
			leftBlock = true;

		
		if (CollisionMode == CollisionDetectionMode.medium) 
		{
			if (currentPoint.columnIndex == column + 1 && currentPoint.rowIndex == row - 1) {
				if (upBlock && rightBlock)
					return false;
			} else if (currentPoint.columnIndex == column - 1 && currentPoint.rowIndex == row + 1) {
				if (leftBlock && downBlock)
					return false;
			} else if (currentPoint.columnIndex == column - 1 && currentPoint.rowIndex == row - 1) {
				if (upBlock && leftBlock)
					return false;
			} else if (currentPoint.columnIndex == column + 1 && currentPoint.rowIndex == row + 1) {
				if (rightBlock && downBlock)
					return false;
			}
		} 
		else if (CollisionMode == CollisionDetectionMode.hard) 
		{
			if ((upBlock && rightBlock) || (upBlock && leftBlock) || (downBlock && rightBlock) || (downBlock && leftBlock))
				return false;
		}

		return true;
	}


	//#################################################################
	//##### 8 neighbors of current point Ignore Unwalkale tile ########
	//#################################################################

	private void FindNeighbors_IgnoreContact(node node_)
	{
		int row = node_.rowIndex;
		int column = node_.columnIndex;

		if (column - 1 >= 0) 
		{
			if (row - 1 >= 0) 
			{
				if (!closList.Contains(NodeArray [row - 1, column - 1])) {
					AddToOpenList (NodeArray [row - 1, column - 1]);
				}
			}

			if (!closList.Contains(NodeArray [row, column - 1])) 
			{
				AddToOpenList (NodeArray [row , column - 1]);
			}

			if (row + 1 < map_height)
			{
				if (!closList.Contains(NodeArray [row + 1, column - 1])) {
					AddToOpenList (NodeArray [row + 1, column - 1]);
				}
			}
		}

		if(row-1>=0)
		{
			if (!closList.Contains(NodeArray [row - 1, column])) 
			{
				AddToOpenList (NodeArray [row - 1, column]);
			}
		}

		if(row+1<map_height)
		{
			if (!closList.Contains(NodeArray [row + 1, column])) 
			{
				AddToOpenList (NodeArray [row + 1, column ]);
			}
		}

		if (column + 1 <map_width) 
		{
			if (row - 1 >= 0) 
			{
				if (!closList.Contains(NodeArray [row - 1, column + 1])) {
					AddToOpenList (NodeArray [row - 1, column + 1]);
				}
			}

			if (!closList.Contains(NodeArray [row, column + 1])) 
			{
				AddToOpenList (NodeArray [row , column + 1]);
			}

			if (row + 1 < map_height)
			{
				if (!closList.Contains(NodeArray [row + 1, column + 1])) {
					AddToOpenList (NodeArray [row + 1, column + 1]);
				}
			}
		}
	}

	//###########################################
	//##### Adding neighbors to openlist ########
	//###########################################

	private void AddToOpenList(node node_)
	{	
		if (!openList.Contains (node_)) 
		{
			openList.Add (node_);
			node_.parent = currentPoint;
			Find_F_Cost (node_);
			return;
		}
		int newG;
		if (node_.rowIndex == currentPoint.rowIndex || node_.columnIndex == currentPoint.columnIndex) 
			newG = currentPoint.g + 10;
		else 
			newG = currentPoint.g + 14;
		if(newG<node_.g)
		{	
			node_.parent = currentPoint;
			Find_F_Cost (node_);
		}
	}

	//###################################
	//##### Find F movement cost ########
	//###################################

	private void Find_F_Cost(node node_)
	{
		if (node_.parent == null) 
		{
			node_.g = 0;
			node_.h = Find_H_Manhatan(node_);
			node_.f = node_.g + node_.h ;
			return;
		}
		node parent = node_.parent;
		if (node_.rowIndex == currentPoint.rowIndex || node_.columnIndex == currentPoint.columnIndex) 
		{
			node_.g = 10 + parent.g;
		} 
		else
		{
			node_.g = 14 + parent.g;
		}
		node_.h = Find_H_Manhatan (node_);
		node_.f = node_.h + node_.g;
	}

	//############################################################
	//##### Find H movement cost usint manhathan algoritm ########
	//############################################################

	private int Find_H_Manhatan(node node_)
	{
		int vertical_dist = Mathf.Abs (TargetPoint.rowIndex - node_.rowIndex);
		int horizontal_dist = Mathf.Abs (TargetPoint.columnIndex - node_.columnIndex);
		return (vertical_dist + horizontal_dist)*10;
	}

	//########################################
	//##### Find Lowese F in openlist ########
	//########################################

	private node FindLowestF()
	{
		int lowest_f = 10000000;
		node lowFNode = new node ();
		foreach(node tmp in openList)
		{
			if (tmp.f < lowest_f) {
				lowFNode = tmp;
				lowest_f = tmp.f;
			}
		}
		return lowFNode;
	}



	//##########vvvvvvvvvvvvv#####################vvvvvvvvvvvv############
	//#####                         RESULTS                       ########
	//#########################vvvvvvvvvvvvvv#############################
	 


	//####################################################################
	//##### Use This Method For Getting Result Node of found path ########
	//####################################################################

	public List<node> PathNodes()
	{	
		//Debug.Log ("closeList count :"+closList.Count);
		List<node> pathNodeList = new List<node> ();
		node node_ = TargetPoint;
		while (node_!=StartPoint && node_.parent!=null) 
		{	
			pathNodeList.Add (node_);
			node_ = node_.parent;

		}
		pathNodeList.Add (StartPoint);
		pathNodeList.Reverse ();
		return pathNodeList;
	}

	//#########################################################################
	//##### Use This Method For Getting Result Positions of found path ########
	//#########################################################################

	public List<Vector2> PathPoses()
	{	
		Debug.Log ("closeList count :"+closList.Count);
		List<Vector2> pathNodeList = new List<Vector2> ();
		node node_ = TargetPoint;
		while (node_!=StartPoint  && node_.parent!=null) 
		{	
			pathNodeList.Add (node_.pos);
			node_ = node_.parent;

		}
		pathNodeList.Add (StartPoint.pos);
		pathNodeList.Reverse();
		return pathNodeList;
	}

	//###########################################################
	//###### This method will return all node positions #########
	//###########################################################

	public List<Vector2> GetNodesPos()
	{
		List<Vector2> mapNodePoses = new List<Vector2> ();
		foreach(node tmp in NodeArray)
		{
			mapNodePoses.Add (tmp.pos);
		}
		mapNodePoses.Reverse();
		return mapNodePoses;
	}

	//####################################################
	//##### this method will return all map nodes ########
	//####################################################

	public node[,] GetMapNodes()
	{
		return NodeArray;
	}
}
