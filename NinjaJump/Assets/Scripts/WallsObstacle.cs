using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsObstacle : MonoBehaviour {

	public Rigidbody2D rbobs;
	private bool genrateWalls;
	public float destroyTime;

	void Start (){
		if (Generate.Stage == 2) destroyTime = 7f;
		else destroyTime = 8f;

		StartCoroutine (destroyWalls());
		genrateWalls = false;

	}

	// Use this for initialization
	void Update()
	{   
		if (MyConstant.gamePlay == false) {
			Vector2 velocity1 = new Vector2(0, 7);
			rbobs.velocity = velocity1;
		}else rbobs.velocity = MyConstant.velocity;
		if (genrateWalls == false && rbobs.position.y< -6f) {genrateWalls = true;
				Instantiate (MyConstant.rocks);
		}
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);


	}

	IEnumerator destroyWalls(){
		
		yield return new WaitForSeconds(destroyTime);
		Destroy(this.gameObject);
	}
}
