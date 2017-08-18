using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birds : MonoBehaviour {
	Animator anim;
	Vector3 PointA;
	Vector3 PointB;
	Vector3 PointC;
	private static bool onPosA;
	private static float cureenttime;
	private static float total;
	public int ran;
	private static bool posDown;
	private static bool birdWait;
	private static float walking;
	private static AudioSource s;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		PointB = new Vector3 (2.4f, -3f, 52.4f);
		PointC = new Vector3 (-2.1f, -3f, 52.4f);
		posDown = false;
		birdWait = true;
		MyConstant.bird_point = false;
		walking = .1f;
		MyConstant.instanciateBird = true;
		s = anim.GetComponent<AudioSource> ();
		s.Play ();
	}

	IEnumerator Birds(){
		yield return new WaitForSeconds (.5f);
		PointA = anim.transform.position;
		cureenttime = 0;
		total = 1f;
		onPosA = true;
		MyConstant.bird_point = true;
		Generate.AfterBirdGenerate = true;
			}

	// Update is called once per frame
	void Update () {

		if (anim.GetBool ("flyDestroy") == true) 
			birdWait = false;
			//StartCoroutine (destroyBirds());

		if (birdWait) {
			Vector3 v = anim.transform.position;
			v.y -= walking;
		    transform.position = v;
			//if(v.y <=-2.5)
			//	StartCoroutine (destroyBirds());

			if (posDown == false && v.y <= 3) {
				birdWait = false;
				StartCoroutine (Birds ());
				if (ran == 1) {transform.rotation = Quaternion.Euler (new Vector3 (0,176.34f,45f));
				}
				else if (ran == 2) {
					transform.rotation = Quaternion.Euler (new Vector3 (0, 0,45f));
				}
			} 
		}

		if (onPosA == true)
		{
			cureenttime += Time.deltaTime;
			float per = cureenttime / total;
			if (ran == 1) {
				anim.transform.position = Vector3.Lerp (PointA, PointB, per);
			}else if(ran ==2){
				anim.transform.position = Vector3.Lerp (PointA, PointC, per);
			}
			if(cureenttime>=total)
			{
				onPosA = false;
				MyConstant.bird_point = false;
				if (ran == 1) {
					transform.rotation = Quaternion.Euler (new Vector3 (0f, 176.809f, 0f));
				}
				else if (ran == 2) {
					transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
				}
				walking = .05f;
				birdWait = true;
				posDown = true;
				s.Stop ();
				}
		}

	}

	IEnumerator destroyBirds(){

		yield return new WaitForSeconds(1f);
		Destroy(this.gameObject);
		MyConstant.instanciateBird = false;
	}
}
