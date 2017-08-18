using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonousFontAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 v = transform.position;
		v.y+=30;
		transform.position = v;
	}
}
