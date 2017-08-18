using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine.UI;
public class Test : MonoBehaviour {
	
	private static System.DateTime startDate ;
	private static System.DateTime today ;

	public static int LuncherAdStartday;
	public static int LuncherAdType;
	public static String LuncherAdId;
	public static int LuncherAdOccurance;

	public static int GameAdStartday;
	public static int GameAdType;
	public static String GameAdId;
	public static int GameAdOccurance;

	public static int FailAdStartday;
	public static int FailAdType;
	public static String FailAdId;
	public static int FailAdOccurance;

	public static int PauseAdStartday;
	public static int PauseAdType;
	public static String PauseAdId;
	public static int PauseAdOccurance;

	public static int MainAdStartday;
	public static int MainAdType;
	public static String MainAdId;
	public static int MainAdOccurance;

	public static String RateIt;
	public static String More;
	public static String FbLink;

	public static int installationDays;

	public static int pauseoccur;
	public static int failoccur;
	private static  bool Luncher;

	public static GoogleMobileAdsDemoScript googleads;
	// Use this for initialization
	void Start () {

		if (PlayerPrefs.GetInt ("Luncher") == 0)
			PlayerPrefs.SetInt ("Luncher", 0);

		if(PlayerPrefs.GetString ("ServerAdsData").Length<=0){
			string s="<NJ_DB_FULL>0#3#ca-app-pub-3451337100490595/6491126430#3</NJ_DB_FULL><URL>https://play.google.com/store/apps/details?id=com.app.ninja#https://play.google.com/store/apps/developer?id=Quantum4u&hl=en#https://www.facebook.com</URL>\n <NJ_GAME>0#1#ca-app-pub-3451337100490595/7630137659#1</NJ_GAME>\n<NJ_FAIL>0#3#ca-app-pub-3451337100490595/9370417123#1</NJ_FAIL>\n<NJ_PAUSE>0#3#ca-app-pub-3451337100490595/6491126430#2</NJ_PAUSE>\n<NJ_DASHBOARD>0#1#ca-app-pub-3451337100490595/4066119550#0</NJ_DASHBOAD>";
			PlayerPrefs.SetString("ServerAdsData",s) ;}

		PlayerPrefs.SetInt("FailOccurLocal",  0) ;
		PlayerPrefs.SetInt("PauseOccurLocal",  0) ;

			string url = "http://quantum4you.com/piqvalue.php?val=NINJA_GAME2";
			WWW www = new WWW (url);
			StartCoroutine (WaitForRequest (www));
	
		googleads = gameObject.AddComponent<GoogleMobileAdsDemoScript> ();

		if (PlayerPrefs.GetString ("ServerAdsData").Length > 10)  AdsDataCalculation (PlayerPrefs.GetString ("ServerAdsData"));
		installationDays = int.Parse(GetDaysPassed());

		if (MainAdStartday == 0 || (MainAdStartday >= installationDays)) {
			adsCalling (MainAdType, MainAdId);
		}

		if (Luncher==false && (LuncherAdStartday==0 ||(LuncherAdStartday >= Test.installationDays)) && LuncherFullDisplay (LuncherAdOccurance)) {
			adsCalling (LuncherAdType, LuncherAdId);
			StartCoroutine (AdsFullRequest ());
		}
		if (Luncher == false)
			Luncher = true;
	}
	IEnumerator AdsFullRequest(){
		
		yield return new WaitForSeconds (2f);
			adsCallingrewardFull (LuncherAdType , LuncherAdId);
     }
	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		if (www.error == null)
		{
			PlayerPrefs.SetString("ServerAdsData",  www.text) ;
			AdsDataCalculation (PlayerPrefs.GetString ("ServerAdsData"));
		} else {
			//Debug.Log("WWW Error: "+ www.error);
		}    
	}

	private void AdsDataCalculation(String data){
		String str1 = substring(data,"<NJ_GAME>","</NJ_GAME>",9);
		String str2 = substring(data,"<NJ_FAIL>","</NJ_FAIL>",9);
		String str3 = substring(data,"<NJ_PAUSE>","</NJ_PAUSE>",10);
		String str4 = substring(data,"<NJ_DASHBOARD>","</NJ_DASHBOARD>",14);
		String str5 = substring(data,"<URL>","</URL>",5);
		String str6 = substring (data, "<NJ_DB_FULL>", "</NJ_DB_FULL>", 12);

		string[] splitstr1 = str1.Split ('#');
		string[] splitstr2 = str2.Split ('#');
		string[] splitstr3 = str3.Split ('#');
		string[] splitstr4 = str4.Split ('#');
		string[] splitstr5 = str5.Split ('#');
		string[] splitstr6 = str6.Split ('#');
		//Debug.Log ("parsing prblm..." +str6+"  "+ splitstr6 [0]);
		/***************Ads Start Day **********************/
		GameAdStartday = int.Parse(splitstr1[0]);
		FailAdStartday = int.Parse(splitstr2[0]);
		PauseAdStartday =int.Parse(splitstr3[0]);
		MainAdStartday = int.Parse(splitstr4[0]);
		LuncherAdStartday = int.Parse(splitstr6[0]);
		/***************Ads type **********************/
		GameAdType = int.Parse(splitstr1[1]);
		FailAdType = int.Parse(splitstr2[1]);
		PauseAdType =int.Parse(splitstr3[1]);
		MainAdType = int.Parse(splitstr4[1]);
		LuncherAdType = int.Parse(splitstr6[1]);
		/***************Ads id **********************/
		GameAdId = splitstr1[2];
		FailAdId= splitstr2[2];
		PauseAdId =splitstr3[2];
		MainAdId = splitstr4[2];
		LuncherAdId = splitstr6 [2];
		/***************Ads occurance **********************/

		RateIt = splitstr5 [0];
		More = splitstr5 [1];
		FbLink = splitstr5 [2];

		GameAdOccurance = int.Parse(splitstr1[3]);
		FailAdOccurance = int.Parse(splitstr2[3]);
		PauseAdOccurance =int.Parse(splitstr3[3]);
		MainAdOccurance = int.Parse(splitstr4[3]);
		LuncherAdOccurance = int.Parse(splitstr6[3]);

	}

	public static  bool LuncherFullDisplay(int server_val){
		
		int local_val = PlayerPrefs.GetInt ("Luncher");
		local_val++;
		//Debug.Log ("After Value...." + local_val+"   "+local_val % server_val+"   "+server_val);
		if(server_val == local_val)
			PlayerPrefs.SetInt("Luncher",  0) ;
		else
			PlayerPrefs.SetInt("Luncher",  local_val) ;
		
		if (local_val% server_val == 0)
			return true;
		else
			return false;

	}

	private String substring(String data , String starttag , String Endtag, int size){

		data = data.Trim ();
		int endpos = 0;
		int endindex = 0;

		if (data.IndexOf (starttag) > -1) endpos = (data.IndexOf (starttag)+size);
		else endpos=(size+1);

		if (data.IndexOf (Endtag) >0) endindex = (data.IndexOf(Endtag)-(endpos));
		else endindex =data.Length-((data.IndexOf(starttag))+(2*size));

		String s= data.Substring (data.IndexOf(starttag)+size , endindex);
		return s;
	}

	private static void SetStartDate()
	{
		
		if(PlayerPrefs.HasKey("DateInitialized")) //if we have the start date saved, we'll use that
			startDate = System.Convert.ToDateTime(PlayerPrefs.GetString("DateInitialized")) ;
		else 
		{	
			startDate = System.DateTime.Now ; //save the start date ->
			PlayerPrefs.SetString("DateInitialized", startDate.ToString()) ;
		}
	}

	public static string GetDaysPassed()
	{    SetStartDate() ;
		today = System.DateTime.Now ;

		//days between today and start date -->

		System.TimeSpan elapsed = today.Subtract(startDate) ;

		double days = elapsed.TotalDays ;
		return days.ToString("0") ;
	}


	public static void adsCalling(int Type , String id){

		if (Type == 1) {	
			googleads.RequestBanner (GoogleMobileAds.Api.AdPosition.Bottom, id);
		} else if (Type == 2) {
			googleads.RequestNativeExpressAdView (id);
		}else if (Type == 3) {
			googleads.RequestInterstitial (id);
		}else if (Type == 4 ) {
			googleads.RequestRewardBasedVideo (id);
		}

	}

	public static void adsCallingrewardFull(int Type, string id /*,int occurances ,int occur*/ ){

        if (Type == 3) 
				googleads.ShowInterstitial (id);
		else if (Type == 4) 
				googleads.ShowRewardBasedVideo (id);
	}
	public static void adsDestroy(int Type ){
		try{

		if (Type == 1) {
			if(Test.googleads.bannerView!=null)
			googleads.bannerView.Destroy();
		} else if (Type == 2) {
			if(Test.googleads.nativeExpressAdView!=null)
		    googleads.nativeExpressAdView.Destroy ();
		}
		}
		catch(Exception e){
		}

	}
	// Update is called once per frame
	void Update () {
		
	}


	}
