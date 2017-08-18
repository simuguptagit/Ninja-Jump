using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newArrowthrow : MonoBehaviour {
	Animator anim;
	public GameObject Arrow;
	public GameObject Arrow1;
	public GameObject Arrow2;
	public GameObject Arrow3;

	private static float cureenttime;
	private static float total;
	public int ran;

	private int Arrowthrow;
	Vector3 PointA;
	Vector3 PointB;
	Vector3 PointC;
	Vector3 PointD;
	Vector3 PointE;

	Vector3 left;
	Vector3 right;

	private static AudioSource ArrowSound;
	private bool walking;
	private bool walkStop;
	private float walk;

	void Start () {
		anim = GetComponent<Animator>();

		PointB = new Vector3 (3.5f, -2f, 52.4f);
		PointC = new Vector3 (3.5f, -6f, 52.4f);

		PointD = new Vector3 (-3.5f, -2f, 52.4f);
		PointE = new Vector3 (-3.5f, -6f, 52.4f);
		MyConstant.instanciateArrow = true;
		cureenttime = 0;
		walk = .1f;
		if (MyConstant.velocity.y >= -7) {
			total = .9f;walk = .10f;
		} else if (MyConstant.velocity.y >= -9) {
			total = .7f;walk = .11f;
		} else if (MyConstant.velocity.y >= -11) {
			total = .5f;walk = .11f;
		}
		ArrowSound = anim.GetComponent<AudioSource> ();
		ArrowSound.Stop ();
		Arrowthrow = 0;
		left = new Vector3 (-1.5f, 3f, 52.4f);
		right = new Vector3 (1.5f, 3f, 52.4f);
		walking = true;


	}

	IEnumerator Arrowchange(){
		yield return new WaitForSeconds (.4f);
		Arrow2.SetActive (true);
		Arrow3.SetActive (true);
		anim.SetBool("throwarrow" , true);
		yield return new WaitForSeconds (.2f);
		Arrowthrow = 1;
		anim.SetBool("throwarrow" , false);
		ArrowSound.Play ();
	}

	// Update is called once per frame
	void Update () {
		if (walking) {
			Vector3 v = anim.transform.position;
			v.y -= walk;
			transform.position = v;

			if(v.y <=-3)
				StartCoroutine (destroyWalls());
			
			if (walkStop==false && v.y <=3.5) {
				walking = false;
				walkStop = true;
				StartCoroutine (Arrowchange ());

			} 
		}

		if (Arrowthrow == 1 ) {
			
			Arrowthrow = 2;

			Arrow2.SetActive (false);
			Arrow3.SetActive (false);
			if (ran == 1) {
				Arrow.transform.position = left;
				Arrow1.transform.position = left;
			} else {
				Arrow.transform.position = right;
				Arrow1.transform.position = right;
			}
			PointA = Arrow.transform.position;

			if(Generate.Stage == 3) Arrow.SetActive (true);
			Arrow1.SetActive (true);
		
		}

		if (Arrowthrow == 2) {
			cureenttime += Time.deltaTime;
			float per = cureenttime / total;
			if (ran == 1) {
				if(Generate.Stage == 3)Arrow.transform.position = Vector3.Lerp (PointA, PointB, per);
				Arrow1.transform.position = Vector3.Lerp (PointA, PointC, per);
			} else if (ran == 2) {
				if(Generate.Stage == 3) Arrow.transform.position = Vector3.Lerp (PointA, PointD, per);
				Arrow1.transform.position = Vector3.Lerp (PointA, PointE, per);
			}
			if (cureenttime >= total) {	
				walk += .05f;
				walking = true;
				Arrowthrow = 3;
				ArrowSound.Stop ();
				Generate.AfterArrowGenerate = true;
			}
		}
	}

	IEnumerator destroyWalls(){

		yield return new WaitForSeconds (.5f);
		Destroy (this.gameObject);
		Destroy (Arrow);
		Destroy (Arrow1);
		MyConstant.instanciateArrow = false;
	}
}
