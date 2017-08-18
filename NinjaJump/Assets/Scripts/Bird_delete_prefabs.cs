using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_delete_prefabs : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		StartCoroutine (destroyBirds());
	}

	IEnumerator destroyBirds(){
		
		yield return new WaitForSeconds(4f);
		Destroy(this.gameObject);
		MyConstant.instanciateBird = false;
	}
}
