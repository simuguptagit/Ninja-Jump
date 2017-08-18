using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	private Vector2 velocity = new Vector2(0, -5);
	public Rigidbody2D rbobs;
	//public float range;

	void Start (){
		StartCoroutine (destroyWalls());
	
	}

	// Use this for initialization
	void Update()
	{   
		 if (MyConstant.gamePlay == false) {
			Vector2 velocity1 = new Vector2 (0, 7);
			rbobs.velocity = velocity1;
		}

		else {
			rbobs.velocity = MyConstant.velocity;
		}
			transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		 
	}

	IEnumerator destroyWalls(){
	//	Debug.Log("wall stopingg...");
		yield return new WaitForSeconds(5f);
		Destroy(this.gameObject);
	}
}
