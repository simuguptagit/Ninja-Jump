using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseAndResume : MonoBehaviour {

	public static bool pause;
	public GameObject paneel;
	public GameObject FailPanel;
	public GameObject timeCounter;
	public AudioSource bgsound;
	public  Text countText;
	public GameObject soundplay;
	public GameObject soundmute;
	public GameObject Pause;
	// Use this for initialization
	void Start () {
		pause = false;	
	}
	
	// Update is called once per frame
	void Update () {
		}

	public void onPause(){
		if ((Test.PauseAdStartday ==0 ||(Test.PauseAdStartday <= Test.installationDays)) && NinjaCollisionDetection.isPauseDisplay(Test.PauseAdOccurance)) Test.adsCallingrewardFull (Test.PauseAdType, Test.PauseAdId);
		if ((Test.PauseAdStartday==0||(Test.PauseAdStartday <= Test.installationDays)) && NinjaCollisionDetection.isPauserequest(Test.PauseAdOccurance)) Test.adsCalling (Test.PauseAdType, Test.PauseAdId);

		SoundManagerScript.run.Stop();
		bgsound.Stop ();
		if(MyConstant.gamePlay==true){
			timeCounter.SetActive (false);
		pause = !pause;
		if (pause) {
				Pause.SetActive (false);
			if (PlayerPrefs.GetInt ("sound") == 1) {
				soundmute.SetActive (false);
				soundplay.SetActive (true);
			} else if (PlayerPrefs.GetInt ("sound") == 2) {
				soundplay.SetActive (false);
				soundmute.SetActive (true);
			}
			Time.timeScale = 0;

			paneel.gameObject.SetActive (true);
			
			countText.text = Bg_ScrollingController.count.ToString ();
		}
	  }
	}

	public void onResume(){
		SoundManagerScript.run.Play();
		bgsound.Play ();
		pause = false;
		if (!pause) {
			Pause.SetActive (true);
			Time.timeScale = 1;
			paneel.gameObject.SetActive(false);
		} 
	}

	public void onSound(){
		if (PlayerPrefs.GetInt ("sound") == 1) {
			PlayerPrefs.SetInt ("sound", 2);
			soundmute.SetActive(true);
			soundplay.SetActive(false);
			AudioListener.volume =0;
		}
		else if (PlayerPrefs.GetInt ("sound") == 2) {
			PlayerPrefs.SetInt ("sound", 1);
			soundplay.SetActive(true);
			soundmute.SetActive(false);
			AudioListener.volume =1;
		}
	
	}

	public void onMainPage(){
		if (Test.GameAdStartday==0||(Test.GameAdStartday <= Test.installationDays)) Test.adsDestroy (Test.GameAdType);
		pause = false;
		Time.timeScale = 1;
		paneel.gameObject.SetActive(false);
		SceneManager.LoadScene (0); 
	}

	public  void onreplay(){
		FailPanel.gameObject.SetActive(false);
		Pause.SetActive (true);
		SceneManager.LoadSceneAsync(1);
		Time.timeScale = 1;
		SceneManager.LoadScene(1);
	}
}
