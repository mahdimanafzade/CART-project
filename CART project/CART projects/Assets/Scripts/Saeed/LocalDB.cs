using UnityEngine;
using System.Collections;
using SimpleJSON;

public class LocalDB : DataBase {


	public static int currentLevel 
	{
		get
		{ 
			return int.Parse( AES.Decrypt (PlayerPrefs.GetString("currentLevel").ToString()));
		}
		set
		{ 
			PlayerPrefs.SetString("currentLevel",AES.Encrypt(value.ToString()));
		}
	}

	public static string playerCards
	{
		get
		{ 
			string s = AES.Decrypt( PlayerPrefs.GetString("playerCards") );
			if(s==".")
				s="";
			return s;
		}
		set
		{ 
			if(value=="")
				value=".";
			PlayerPrefs.SetString ("playerCards",AES.Encrypt( value) );
		}
	}

	public static string enemyCards
	{
		get
		{ 
			string s =  AES.Encrypt( PlayerPrefs.GetString("enemyCards") );
			if(s==".")
				s="";
			return s;
		}
		set
		{ 
			if(value=="")
				value=".";
			PlayerPrefs.SetString ("enemyCards",AES.Encrypt( value) );
		}
	}


	public static string AllLevels
	{
		get
		{ 
			string s = AES.Decrypt( PlayerPrefs.GetString("AllLevels") );
			if(s==".")
				s="";
			return s;
		}
		set
		{ 
			if(value=="")
				value=".";
			PlayerPrefs.SetString ("AllLevels",AES.Encrypt( value) );
		}
	}

	public static string PlayerName
	{
		get
		{ 
			string s = AES.Decrypt( PlayerPrefs.GetString("PlayerName") );
			if(s==".")
				s="";
			return s;
		}
		set
		{ 
			if(value=="")
				value=".";
			PlayerPrefs.SetString ("PlayerName",AES.Encrypt( value) );
		}
	}

	public static string PlayerEmail
	{
		get
		{ 
			string s =  AES.Decrypt( PlayerPrefs.GetString("PlayerEmail") );
			if(s==".")
				s="";
			return s;
		}
		set
		{ 
			if(value=="")
				value=".";
			PlayerPrefs.SetString ("PlayerEmail",AES.Encrypt( value) );
		}
	}

	public static string PlayerPassword
	{
		get
		{ 
			string s = AES.Decrypt( PlayerPrefs.GetString("PlayerPassword") );
			if(s==".")
				s="";
			return s;
		}
		set
		{ 
			if(value=="")
				value=".";
			PlayerPrefs.SetString ("PlayerPassword",AES.Encrypt( value) );
		}
	}

	public int PlayerAvataIndex
	{
		get
		{ 
			return int.Parse( AES.Decrypt (PlayerPrefs.GetString("PlayerAvataIndex").ToString()));
		}
		set
		{ 
			PlayerPrefs.SetString("PlayerAvataIndex",AES.Encrypt(value.ToString()));
		}
	}

	public static string playerPhoneNumber
	{
		get
		{ 
			string s = AES.Decrypt( PlayerPrefs.GetString("playerPhoneNumber") );
			if(s==".")
				s="";
			return s;
		}
		set
		{ 
			if(value=="")
				value=".";
			PlayerPrefs.SetString ("playerPhoneNumber",AES.Encrypt( value) );
		}
	}


	public static int playerWinCount
	{
		get
		{ 
			return int.Parse( AES.Decrypt (PlayerPrefs.GetString("playerWinCount").ToString()));
		}
		set
		{ 
			PlayerPrefs.SetString("playerWinCount",AES.Encrypt(value.ToString()));
		}
	}

	public static int playerlossCount
	{
		get
		{ 
			return int.Parse( AES.Decrypt (PlayerPrefs.GetString("playerlossCount").ToString()));
		}
		set
		{ 
			PlayerPrefs.SetString("playerlossCount",AES.Encrypt(value.ToString()));
		}
	}

	public static int PlayerScore
	{
		get
		{ 
			return int.Parse( AES.Decrypt (PlayerPrefs.GetString("PlayerScore").ToString()));
		}
		set
		{ 
			PlayerPrefs.SetString("PlayerScore",AES.Encrypt(value.ToString()));
		}
	}

	public static int PlayerCoins
	{
		get
		{ 
			return int.Parse( AES.Decrypt (PlayerPrefs.GetString("PlayerCoins").ToString()));
		}
		set
		{ 
			PlayerPrefs.SetString("PlayerCoins",AES.Encrypt(value.ToString()));
		}
	}

	public static int PlayerJems
	{
		get
		{ 
			return int.Parse( AES.Decrypt (PlayerPrefs.GetString("PlayerJems").ToString()));
		}
		set
		{ 
			PlayerPrefs.SetString("PlayerJems",AES.Encrypt(value.ToString()));
		}
	}




	public static JSONNode JsonForThisLevel(int levelIndex)
	{
		JSONNode allLevels = JSON.Parse (LocalDB.AllLevels);
		return allLevels["AllLevels"][levelIndex];
	} 
}
