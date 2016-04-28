using UnityEngine;
using System.Collections;
using SimpleJSON;

public class LevelsDefiner : MonoBehaviour {

	public levelInfo[] allLevels;

	void Awake()
	{	
		ResetGameInfo ();
		CreatLevelJson ();
	}

	void ResetGameInfo()
	{
		LocalDB.PlayerScore = 0;
		LocalDB.PlayerName = "";
		LocalDB.playerPhoneNumber = "";
		LocalDB.playerCards = "";
		LocalDB.PlayerCoins = 1000;
		LocalDB.playerWinCount = 0;
		LocalDB.playerlossCount = 0;
		LocalDB.currentLevel = 0;
	}

	public void CreatLevelJson()
	{	
		JSONClass A = new JSONClass ();
		for(int i=0; i<allLevels.Length ;i++)
		{
			A["AllLevels"].Add( CrateThisLevel (allLevels[i]) );
		}
		LocalDB.AllLevels = A.ToString ();
		print (A.ToString());
	}

	JSONNode CrateThisLevel(levelInfo level)
	{
		JSONClass N = new JSONClass();
		N ["supportCount"].AsInt = level.supportCount;
		N ["soldeirCount"].AsInt = level.supportCount;
		return N;
	}

}
