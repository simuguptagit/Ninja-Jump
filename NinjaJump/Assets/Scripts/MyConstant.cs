using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyConstant{

	public static string Level1="L1";
	public static bool powerinstantiate;
	public static bool gamePlay;
	public static float scrollSpeed;
	public static float tileSizeZ;
	public static bool Ladders_Velocity_inStart;

	public static float RepeatWallTime;
	public static bool falling;
	public static int fall;
	public static float NomalWallSpeed;
	public static Vector2 velocity;

	public static bool instanciateBird;
	public static bool instanciateArrow;
	public static bool power;
	public static int powerMode2;
	public static bool bird_point;

	public static bool serverHit;
	public static float modelmovingspeed;


	public static List<GameObject> List1 = new List<GameObject>();
	public static List<GameObject> tmpList1 = new List<GameObject>();
	public static List<GameObject> List2 = new List<GameObject>();
	public static List<GameObject> List3 = new List<GameObject>();
	public static GameObject  testrocks;
	public static GameObject  rocks;
	public static GameObject  bird_obstacle1;
	public static GameObject  bird_obstacle2;

	public static void getlevel1(string Level){
		if (Level.Equals (Level1)) {
			
			if (PlayerPrefs.GetInt ("sound") == 1) {
				AudioListener.volume = 1;
			} else if (PlayerPrefs.GetInt ("sound") == 2) {
				AudioListener.volume = 0;
			}
				/*...............ads preferences...........*/

			/*...........Game Preferences...........*/
			if (PlayerPrefs.GetInt ("bestHeight") == 0)
				PlayerPrefs.SetInt ("bestHeight", 0);
			if (PlayerPrefs.GetInt ("sound") == 0)
				PlayerPrefs.SetInt ("sound", 1);

			/*.........4r bg.........*/
			gamePlay = true;
			scrollSpeed = .05f;
			tileSizeZ = 14;
			powerinstantiate = false;

			/*.....4r walls and ladders......*/
			RepeatWallTime = 3f;
			NomalWallSpeed = 1.2f;
			velocity = new Vector2 (0, -1);
			modelmovingspeed = 10f;
			instanciateBird = false;
			instanciateArrow = false;
			/*.............4r obstacle draw interval for hardnes.....*/
			GameObject[] mygameObjects1 = Resources.LoadAll<GameObject> ("Prefabs_1");
			GameObject[] mygameObjects2 = Resources.LoadAll<GameObject> ("Prefabs_2");
			GameObject[] mygameObjects = Resources.LoadAll<GameObject> ("Prefabs");
			rocks = mygameObjects [0];

			testrocks = mygameObjects [mygameObjects.Length - 1];
			List1.Clear ();
			List2.Clear ();
			List3.Clear ();
			tmpList1.Clear ();

			for (int i = 0; i <= mygameObjects1.Length - 1; i++) {
				if (i < 4) {
					tmpList1.Add (mygameObjects1 [i]);
				}
				List1.Add (mygameObjects1 [i]);
			}
			for (int i = 0; i <20; i++) {
				if (i < 6) {
					//int r = Random.Range (0, 2);
					List2.Add (mygameObjects2 [i]);
				} else if (i >= 6 && i <= 9) {
					int r = Random.Range (6,8);
					List2.Add (mygameObjects2 [r]);
				} else if (i >= 10 && i <= 11) {
					//int r = Random.Range (6, 8);
					List2.Add (mygameObjects2 [i-2]);
				} else if (i >= 12 && i <= 15) {
					int r = Random.Range (10, 12);
					List2.Add (mygameObjects2 [r]);
				} else if (i >= 16 && i <= 19) {
					//int r = Random.Range (10, 12);
					List2.Add (mygameObjects2 [12]);
				}
			}

			for (int i = 1; i <= 20; i++) {
				if (i <= 14)
					List3.Add (mygameObjects [i]);
				else if (i >= 15 && i <= 18) {
					int r = Random.Range (15,17);
					List3.Add (mygameObjects [r]);
				} else if (i <=20)
					List3.Add (mygameObjects [17]);
			//	Debug.Log ("print listtt..." + List3 [i-1]+"   "+i);
			}
		}
	}
	
}
