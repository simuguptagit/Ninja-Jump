using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LusthWalk : MonoBehaviour {
	Vector3 v;
	private static bool walk;
	private Vector3 position;
	private Vector3 position1;
	public GameObject gunfire;
	public GameObject gun;
	private static int fire;
	// Use this for initialization
	void Start () {
		v = transform.position;
		walk = false;
		Generate.AfterLusthGenerate = true;
		transform.rotation = Quaternion.Euler (new Vector3 (0,90f,0));
		StartCoroutine (Gunfire());
		StartCoroutine (destroyLusth());
		position = transform.position;
		fire = 0;
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 movement = new Vector3 (0, 0, 1);
		//Debug.Log ("printtt11111111...."+v.x+"   "+ Vector3.Distance (transform.position, position));
		if (walk == false && Vector3.Distance (transform.position, position) <= 9.5f) {
			transform.Translate (movement * 3 * Time.deltaTime);
		} 
		else if (walk == true && Vector3.Distance (transform.position, position) <=18.5f) {
			transform.Translate (movement * 3 * Time.deltaTime);
		} else {
			walk = true;
			transform.rotation = Quaternion.Euler (new Vector3 (0, -90f, 0));
		}

		if (fire == 1 && gunfire !=null) {
			Vector3 mov = new Vector3 (1, 0, 0);
			if (gunfire.transform.position.x<=2.8f)
			gunfire.transform.Translate (mov *8 * Time.deltaTime);
			else {
				fire = 0;
			    gunfire.SetActive (false);
				StartCoroutine (Gunchange ());
			}
		//	Debug.Log ("print11111..." + gunfire.transform.position+"   "+Vector3.Distance (gunfire.transform.position, position1));	
		}
		if (fire == 2 && gunfire !=null) {
			Vector3 mov = new Vector3 (1, 0, 0);
			if (gunfire.transform.position.x>=-4.5f)
				gunfire.transform.Translate (mov *10 * Time.deltaTime);
			else {
				fire = 0;
				gunfire.SetActive (false);
				StartCoroutine (Gunchange ());
			}
		//	Debug.Log ("print2222..." + gunfire.transform.position+"   "+Vector3.Distance (gunfire.transform.position, position1));	
		}

	}
	IEnumerator Gunchange(){
		yield return new WaitForSeconds (0f);
		//Debug.Log ("walkkk2222..." + walk + "  " + gun.transform.position);
		if(gun != null && gunfire!=null){
		gunfire.transform.position = gun.transform.position;
		Vector3 v = gunfire.transform.position;
		v.x += .8f;
		gunfire.transform.position = v;

		if (walk == false) {
			gunfire.SetActive (true);
			fire = 1;
		}
		if (walk == true) {
			gunfire.transform.rotation = Quaternion.Euler (new Vector3 (0, 180f, 0));
			gunfire.transform.position = gun.transform.position;
			Vector3 v1 = gunfire.transform.position;
			v1.x -= .5f;
			gunfire.transform.position = v1;
			gunfire.SetActive (true);
			fire = 2;
		}

		Vector3 v2 = gunfire.transform.position;
		v2.y -= .1f;
		gunfire.transform.position = v2;
	}
	}
	IEnumerator Gunfire(){
			yield return new WaitForSeconds (1f);
	     gunfire.transform.position = gun.transform.position;
		 gunfire.SetActive (true);
		 position1 = gunfire.transform.position ;

		if(walk == false)
			fire = 1;
		if(walk == true)
			fire = 2;
	}
	IEnumerator destroyLusth(){
		yield return new WaitForSeconds(5f);
		Generate.AfterLusthGenerate = false;
	}
}
