using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oldManStoneThrow : MonoBehaviour {
	public GameObject Stone;
	private int stonethrow;
	private Animator anim;
	private bool ran;
	private float random;

	// Use this for initialization
	void Start () {
		stonethrow = 0;
		anim = GetComponent<Animator> ();
		ran = true;
	}
	// Update is called once per frame
	void Update () {
		Vector3 v = transform.position;
		if (ran == true) {
			random = Random.Range (2.8f,3.1f);
			ran = false;
		}
			if (v.y <= 9 && v.y >= 2.8 && stonethrow == 0) {
				anim.SetBool ("isThrow", true);
				stonethrow = 1;
			}
			if (v.y <= 2.8 && stonethrow == 1) {
				Stone.SetActive (true);
				stonethrow = 0;
				anim.SetBool ("isThrow", false);
			}
		
	}
}
