using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class MAINPageScript : MonoBehaviour {
	public GameObject Loading;
	public GameObject sound;
	public GameObject soundoff;

	public Image loadingbar;
	public float loadingtime;

	public GameObject Dot;
	public GameObject Dot1;
	public GameObject Dot2;
	public GameObject Dot3;
	public GameObject Dot4;
	private int dot;
	private bool loadingbool;

	AsyncOperation async;
	// Use this for initialization
	void Start () {

		loadingbar.fillAmount = 0;
		if (PlayerPrefs.GetInt ("sound") == 1) {
			soundoff.SetActive (false);
			sound.SetActive (true);
		} else if (PlayerPrefs.GetInt ("sound") == 2) {
			sound.SetActive (false);
			soundoff.SetActive (true);
		}

		Test.pauseoccur = 0;
		Test.failoccur = 0;
	}
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown (KeyCode.Escape)) Application.Quit ();
			
		if (loadingbool == true) {
			if( dot == 0) Dot.SetActive (true);
			if (dot == 1) {
				Dot1.SetActive (true);
			} else if (dot == 2) {
				Dot2.SetActive (true);
			} else if (dot == 3) {
				Dot3.SetActive (true);
			} else if (dot == 4) {
				Dot4.SetActive (true);
			}
			else if (dot == 5) {
				Dot.SetActive (false);
				Dot4.SetActive (false);
				Dot3.SetActive (false);
				Dot2.SetActive (false);
				Dot1.SetActive (false);
			}
	
			if (loadingbar.fillAmount <= 1) {
				loadingbar.fillAmount += 1.0f / loadingtime * Time.deltaTime;
			} 
			if(loadingbar.fillAmount >= 1 && async.progress == .9f) {
				async.allowSceneActivation = true;
			}


		}
	}

	IEnumerator PlayChange(){

	//	FbButton.SetActive (false);
	//	RateIt.SetActive (false);

		loadingbool = true;
		Dot.SetActive (true);
		StartCoroutine (DotnoChange());
		Loading.SetActive (true);
		async = SceneManager.LoadSceneAsync(1);
		async.allowSceneActivation = false;

		while (async.isDone == false) {
			yield return null;
		}
	}

	public void Play(){
		
		StartCoroutine (PlayChange());
	
		if (Test.MainAdStartday==0 ||( Test.MainAdStartday >= Test.installationDays)) Test.adsDestroy(Test.MainAdType);
		if (Test.GameAdStartday==0 ||(Test.GameAdStartday >= Test.installationDays)) Test.adsCalling (Test.GameAdType, Test.GameAdId);
	}

	IEnumerator DotnoChange(){
		yield return new WaitForSeconds(.2f);
		if (dot == 0) dot = 1;
		else if (dot == 1) dot = 2;
		else if (dot == 2) dot = 3;
		else if (dot == 3) dot = 4;
		else if (dot == 4) dot = 5;
		else if (dot == 5) dot = 0;
		StartCoroutine (DotnoChange());
	}

	public void Morebtn(){
		Application.OpenURL (Test.More);
	}

	public void fbbtn(){
			Application.OpenURL (Test.FbLink);
	}
	public void Rateit(){
		Application.OpenURL (Test.RateIt);
	}
	public void onSound(){

		if (PlayerPrefs.GetInt ("sound") == 1) {
			PlayerPrefs.SetInt ("sound", 2);
			soundoff.SetActive(true);
			sound.SetActive(false);
			AudioListener.volume =0;
		}
		else if (PlayerPrefs.GetInt ("sound") == 2) {
			PlayerPrefs.SetInt ("sound", 1);
			sound.SetActive(true);
			soundoff.SetActive(false);
			AudioListener.volume =1;
		}
	}
	string body = "Hii,Download this amazing fast and addictive Game   " +
		"https://play.google.com/store/apps/details?id=com.app.ninja";

	public void shareText(){
		AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
		AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
		intentObject.Call<AndroidJavaObject>("setType", "text/plain");
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body);
		AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		currentActivity.Call ("startActivity", intentObject);

	}
}
