using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneRotating : MonoBehaviour {
	public int stoneSide;
	private float value;
	// Update is called once per frame
	void Start () {
		value = .065f;
	//	if(Generate.Stage == 3)
	//		value = .075f;
	}

	void Update () {
		Vector3 v = transform.position;

		if(stoneSide == 1) v.x+=value;
		else if(stoneSide == 2) v.x-=value;
		transform.position = v;
		transform.Rotate (new Vector3(8,8,0)*8f*Time.deltaTime);
	}
}
