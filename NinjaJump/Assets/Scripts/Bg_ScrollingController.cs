using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
public class Bg_ScrollingController : MonoBehaviour {

	public bool scrollndfall;
	private Vector3 startPosition;
	private float currentPosition=0;
	private float startHeightcalculation;
	public  Text countText;
	public static int count;
	public static int bonous;
	private float StartTimeOnLoad=0;
	private static float scoreweight;
	public GameObject bestScore;
	public GameObject timeCounter;
	public static bool bestscoreanim;
	private Animator anim;
	public AudioSource highScoreSound;
	void Start ()
	{
		MyConstant.Ladders_Velocity_inStart = true;
		bestscoreanim = false;
		startPosition = transform.position;
		timeCounter.SetActive (true);

		count = 0;
		SetScore (count);
		StartTimeOnLoad = Time.time;
		Generate.TempList.Clear ();
		scoreweight = .5f;
		StartCoroutine (TimerCounter ());
		StartCoroutine (checkHeight ());
		StartCoroutine (startHeight ());
		highScoreSound.Stop ();
			}

	IEnumerator TimerCounter(){
		yield return new WaitForSeconds(3f);
		timeCounter.SetActive (false);
	}

	IEnumerator checkHeight(){
		yield return new WaitForSeconds(10f);
		scoreweight -= .05f;
		StartCoroutine (checkHeight ());
	}
	IEnumerator countHeight(float x){
		yield return new WaitForSeconds (x);
		count++;
		count += bonous;
		SetScore (count);
		startHeightcalculation += x;
		if(startHeightcalculation<scoreweight) StartCoroutine (countHeight (scoreweight/5));
	}
	IEnumerator startHeight(){
		if (PauseAndResume.pause == false && MyConstant.gamePlay == true) {
			startHeightcalculation = 0;
			StartCoroutine (countHeight (scoreweight/3));
			yield return new WaitForSeconds (scoreweight);
			StartCoroutine (startHeight ());
		}
	}

	public void SetScore(int count){
		countText.text = count.ToString();
		bonous = 0;
		if (bestscoreanim == false && PlayerPrefs.GetInt ("bestHeight")!= 0 && PlayerPrefs.GetInt ("bestHeight") < count) {
			PlayerPrefs.SetInt ("bestHeight", count);
			StartCoroutine (showBestBlinkHeight ());
			bestscoreanim = true;
		}
	}

	IEnumerator showBestBlinkHeight(){
		highScoreSound.Play();
		bestScore.SetActive (true);
		yield return new WaitForSeconds (2f);
		bestScore.SetActive (false);
		highScoreSound.Stop();
	}

	void Update ()
	{   
		if((MyConstant.gamePlay == false ||  Time.timeScale == 0) && highScoreSound.isPlaying == true)
			highScoreSound.Stop();
		
		float newPosition = Mathf.Repeat((Time.time-StartTimeOnLoad )* (MyConstant.scrollSpeed), MyConstant.tileSizeZ);
		if (newPosition>=currentPosition && scrollndfall == true ) {
			currentPosition = newPosition;
		transform.position = startPosition + Vector3.down * newPosition;
		}
		else if (scrollndfall == false ) {
			transform.position = startPosition + Vector3.down * newPosition;

		}
	}

}
