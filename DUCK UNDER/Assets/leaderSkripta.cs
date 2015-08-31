using UnityEngine;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp;    
using com.shephertz.app42.paas.sdk.csharp.user;
using System;
using com.shephertz.app42.paas.sdk.csharp.game;
using UnityEngine.UI;   


public class leaderSkripta : MonoBehaviour {


	public static bool createUser=false;
	public static bool getUserRank=false;
	public static bool getTopNRanks=false;
	public static bool saveScore=false;

	public static string userName;
	public static int gameScore;

	public Text userRank;

	public static string recivedUser=null;
	// Use this for initialization
	void Start () {
		App42API.Initialize("736be5b089d7a57faec17da1d621505c73830242c621c3774408faec99e8ac8b","5decc0b3dcc5bebe463539aa70b082b836d0d42a8d2ed6f43f7e66ce85a42176");  

	}
	
	// Update is called once per frame
	void Update () {

		if (createUser) {
			createUser=false;
			UserService userService = App42API.BuildUserService();  
			userService.CreateUser(userName, "passwordboka", userName+"@gmail.com", new UnityCallBackCreateUser());  
		}

		if (saveScore) {
			saveScore=false;
			ScoreBoardService scoreBoardService = App42API.BuildScoreBoardService();   
			scoreBoardService.SaveUserScore("duckUnder", userName, gameScore, new UnityCallBackSaveScore()); 
		}

		if (getUserRank) {
			getUserRank=false;
			ScoreBoardService scoreBoardService = App42API.BuildScoreBoardService();   
			scoreBoardService.GetUserRanking("duckUnder", userName, new UnityCallBackGetUserRank());  
		}

		if (getTopNRanks) {
			getTopNRanks=false;
			ScoreBoardService scoreBoardService = App42API.BuildScoreBoardService();   
			scoreBoardService.GetTopNRankers("duckUnder", 10, new UnityCallBackGetTopRanks()); 
		}

		if (recivedUser != null) {
			userRank.text=recivedUser;
			recivedUser=null;

		}
	
	}


}

public class UnityCallBackCreateUser : App42CallBack  
{  
	public void OnSuccess(object response)  
	{  
		User user = (User) response;  
		App42Log.Console("userName is " + user.GetUserName());  
		App42Log.Console("emailId is " + user.GetEmail());   
	}  
	public void OnException(Exception e)  
	{  
		App42Log.Console("Exception : " + e);  
	}  
}  

public class UnityCallBackSaveScore : App42CallBack  
{  
	public void OnSuccess(object response)  
	{  
		Game game = (Game) response;       
		App42Log.Console("gameName is " + game.GetName());   
		for(int i = 0;i<game.GetScoreList().Count;i++)  
		{  
			App42Log.Console("userName is : " + game.GetScoreList()[i].GetUserName());  
			App42Log.Console("score is : " + game.GetScoreList()[i].GetValue());  
			App42Log.Console("scoreId is : " + game.GetScoreList()[i].GetScoreId());  
		}  
	}  
	
	public void OnException(Exception e)  
	{  
		App42Log.Console("Exception : " + e);  
	}  
}  

public class UnityCallBackGetUserRank : App42CallBack  
{  
	public void OnSuccess(object response)  
	{  
		Game game = (Game) response;       
		App42Log.Console("gameName is " + game.GetName());   
		for(int i = 0;i<game.GetScoreList().Count;i++)  
		{  
			App42Log.Console("userName is : " + game.GetScoreList()[i].GetUserName());  
			App42Log.Console("rank is : " + game.GetScoreList()[i].GetRank());  
			App42Log.Console("score is : " + game.GetScoreList()[i].GetValue());  
			App42Log.Console("scoreId is : " + game.GetScoreList()[i].GetScoreId());  
			leaderSkripta.recivedUser = game.GetScoreList()[i].GetRank();

		}  
	}  
	
	public void OnException(Exception e)  
	{  
		App42Log.Console("Exception : " + e);  
	}  
}  

public class UnityCallBackGetTopRanks : App42CallBack  
{  
	public void OnSuccess(object response)  
	{  
		Game game = (Game) response;       
		App42Log.Console("gameName is " + game.GetName());   
		for(int i = 0;i<game.GetScoreList().Count;i++)  
		{  
			App42Log.Console("userName is : " + game.GetScoreList()[i].GetUserName());  
			App42Log.Console("score is : " + game.GetScoreList()[i].GetValue());  
			App42Log.Console("scoreId is : " + game.GetScoreList()[i].GetScoreId());  
		}  
	}  
	
	public void OnException(Exception e)  
	{  
		App42Log.Console("Exception : " + e);  
	}  
}  
