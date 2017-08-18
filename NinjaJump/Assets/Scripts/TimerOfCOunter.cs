using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerOfCOunter : MonoBehaviour {
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		StartCoroutine (TimerCounter ());
	}
	IEnumerator TimerCounter(){
		yield return new WaitForSeconds(.5f);
		anim.SetInteger ("Timer", 1);
		yield return new WaitForSeconds(.5f);
		anim.SetInteger ("Timer", 2);
		yield return new WaitForSeconds(.5f);
		anim.SetInteger ("Timer", 3);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
