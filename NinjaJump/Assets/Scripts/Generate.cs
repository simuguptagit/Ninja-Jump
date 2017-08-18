using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour {

	private static float obstcleGenerateTime;
	private static float tempobstcleGenerateTime;
	private static float speed;
	private static float StageInterval;
	public static int  Stage;
	public static bool  Obs;
	public static bool AfterBirdGenerate;
	public static bool AfterLusthGenerate;
	public static bool AfterArrowGenerate;
	public static bool  powermodetime;
	public static bool  StagChangeWithObsGenerate;
	public static List<GameObject> TempList = new List<GameObject>();

// Use this for initialization
	void Start () {
		AfterBirdGenerate = false;
		AfterArrowGenerate = false;
		AfterLusthGenerate = false;
		MyConstant.getlevel1 (MyConstant.Level1);
		StageInterval = 40f;
		tempobstcleGenerateTime = 1.8f;
		Stage = 1;
		Obs = true;
		powermodetime = false;
		StagChangeWithObsGenerate = false;
		StartCoroutine (WaitForWallsDownInStart ());
	}

	IEnumerator WaitForEvery10Seconds(){
		yield return new WaitForSeconds (10f);
	 if (Stage == 2 && MyConstant.velocity.y >= -8f) {
			MyConstant.velocity = MyConstant.velocity + new Vector2 (0, -.6f);
			tempobstcleGenerateTime = obstcleGenerateTime - .3f;
			NinjaCollisionDetection.speed+= .02f;
			NinjaCollisionDetection.powspeed = NinjaCollisionDetection.speed-.9f;
		}
		else if (Stage == 3 && MyConstant.velocity.y >= -9f) {
			MyConstant.velocity = MyConstant.velocity + new Vector2 (0, -0001f);
			tempobstcleGenerateTime = obstcleGenerateTime - .0001f;
			NinjaCollisionDetection.speed+= .01f;
			NinjaCollisionDetection.powspeed = NinjaCollisionDetection.speed-.9f;
		}
		StartCoroutine (WaitForEvery10Seconds ());
	}

	IEnumerator GenerateBirdAfter10sec(){
	yield return new WaitForSeconds(8f);
	for (int i = 0; i < 20; i++) {
			if (i <= 1)
				TempList.Add (MyConstant.List1 [i]);
			else if (i >= 2 && i <= 3) {
				//int r = Random.Range (2, 4);
				TempList.Add (MyConstant.List1 [i]);
			}
			else if (i >= 4 && i <8) {
				int r = Random.Range (4, 6);
				TempList.Add (MyConstant.List1 [r]);
			}
			else if (i >= 8 && i <12) {
				int r = Random.Range (6, 8);
				TempList.Add (MyConstant.List1 [r]);
			}
			else if (i >= 12 && i <14) {
				//int r = Random.Range (8, 10);
				TempList.Add (MyConstant.List1 [i-4]);
			}
			else if (i >= 14 && i <16) {
				//int r = Random.Range (10, 12);
				TempList.Add (MyConstant.List1 [i-4]);
			}
			else if (i >= 16 && i <20) {
				//int r = Random.Range (10, 12);
				TempList.Add (MyConstant.List1 [12]);
			}

	  }

	}

	IEnumerator WaitForWallsDownInStart(){
		yield return new WaitForSeconds(MyConstant.RepeatWallTime);
		MyConstant.velocity = MyConstant.velocity + new Vector2 (0, -5.5f);
		NinjaCollisionDetection.anim.speed = 1.2f;
		StartCoroutine (GenerateObstacle (MyConstant.tmpList1));
		StartCoroutine (GenerateBirdAfter10sec ());
		StartCoroutine (StageIntervalTime());
	}

	IEnumerator StageIntervalTime(){
		yield return new WaitForSeconds(StageInterval);
		StagChangeWithObsGenerate = true;
	}


	IEnumerator GenerateObstacle(List<GameObject> l1){
		obstcleGenerateTime = tempobstcleGenerateTime;
		yield return new WaitForSeconds(obstcleGenerateTime);
		int r1 = Draw ( l1.Count);

		if(MyConstant.gamePlay == true) Instantiate(l1[r1]);

		if (StagChangeWithObsGenerate == true) StageChange ();

		if (Obs == true) {
			
			if (Stage == 1){ 
				if(TempList.Count>0) StartCoroutine (GenerateObstacle (TempList));
				else StartCoroutine (GenerateObstacle (MyConstant.tmpList1));
				}
			if (Stage == 2) StartCoroutine (GenerateObstacle (MyConstant.List2));	
			if (Stage == 3) StartCoroutine (GenerateObstacle (MyConstant.List3));
		}
	}
	void StageChange(){

		if (Stage == 1) {
			TempList.Clear();
			StageInterval =60;
			obstcleGenerateTime = 1.8f;

			Stage = 2;
			StagChangeWithObsGenerate = false;
			StartCoroutine (StageIntervalTime());
			StartCoroutine (WaitForEvery10Seconds ());
		} else if (Stage == 2) {
			Stage = 3;
			obstcleGenerateTime = 1.6f;
			StagChangeWithObsGenerate = false;
		}
	}
	int  Draw( int count){
		int r1 = 0;
	
		if (Stage == 1) {	
				if(count<=4) r1 = Random.Range (0, count);
			    else r1 = Random.Range (0, count);
			while (AfterLusthGenerate == true && r1 >= 16 && r1 <20){
				r1 = Random.Range (0, count);
			}
			while (MyConstant.instanciateBird == true && r1 >= 8 && r1 < 12){
				r1 = Random.Range (0, count);
			}
			while (MyConstant.instanciateArrow == true && r1 >= 4 && r1 < 8){
				r1 = Random.Range (0, count);
			}
			if ((r1 >= 4 && r1 < 12)) Obs = false;
			}
		else if(Stage == 2) {
			r1 = Random.Range (0, count);

			while ((MyConstant.powerinstantiate == true) && r1 >=6 && r1 <10) {
				r1 = Random.Range (0, count);
			}
			while (MyConstant.instanciateBird == true && r1 >= 2 && r1 < 4){
				r1 = Random.Range (0, count);
			}
			while (MyConstant.instanciateArrow == true && r1 >= 4 && r1 < 6){
				r1 = Random.Range (0, count);
			}

			while (AfterLusthGenerate == true && r1 >= 16 && r1 < 20){
				r1 = Random.Range (0, count);
		    }
			if (r1 >= 2 && r1 < 6) Obs = false;

			if (MyConstant.powerinstantiate == false && r1 >= 6 && r1 < 10) {
				MyConstant.powerinstantiate = true;
				StartCoroutine (powermodeintanciateime());
			}
		}
		else if(Stage == 3) {
			r1 = Random.Range (0, count);

			while ((AfterLusthGenerate == true) && r1 >=18 && r1 < 20) {
				r1 = Random.Range (0, count);
			}
			while ((MyConstant.powerinstantiate == true) && r1 >=8 && r1 < 10) {
				r1 = Random.Range (0, count);
			}
			while (MyConstant.instanciateArrow == true && r1 >= 14 && r1 < 18){
				r1 = Random.Range (0, count);
			}
			if (r1 >= 14 && r1 <18) Obs = false;

			if (MyConstant.powerinstantiate == false && r1 >= 8 && r1 < 10) {
				MyConstant.powerinstantiate = true;
				StartCoroutine (powermodeintanciateime());
			}
		}
		return r1;
		}
	IEnumerator powermodeintanciateime(){
		yield return new WaitForSeconds (20f);
		MyConstant.powerinstantiate = false;
	}
		// Update is called once per frame
	void Update () {
		
		if (Obs == false && AfterBirdGenerate == true) {
			AfterBirdGenerate = false;
			Obs = true;
			if (Stage == 1) StartCoroutine (GenerateObstacle (TempList));
			if (Stage == 2) StartCoroutine (GenerateObstacle (MyConstant.List2));	
			if (Stage == 3) StartCoroutine (GenerateObstacle (MyConstant.List3));	
		}

		if (Obs == false && AfterArrowGenerate == true) {
			AfterArrowGenerate = false;
			Obs = true;
			if (Stage == 1) StartCoroutine (GenerateObstacle (TempList));
			if (Stage == 2) StartCoroutine (GenerateObstacle (MyConstant.List2));	
			if (Stage == 3) StartCoroutine (GenerateObstacle (MyConstant.List3));	
		}
	}
}
