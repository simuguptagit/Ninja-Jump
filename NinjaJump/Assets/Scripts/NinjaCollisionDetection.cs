using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class NinjaCollisionDetection : MonoBehaviour {
	public static float speed;
	public static float powspeed;
	public static Animator anim;
	private Vector3 position;
	private bool change;
	private static bool TouchDown;
	private static bool JumpTop;
	public GameObject colisiongameObject;
	public GameObject animpower;
	public static bool PowerMode;

	public AudioSource bgsound;

	public Transform insectparticle ;
	public Transform particle ; 
	public Transform starpowerparticle ; 
	public Transform hutdestroyparticle ; 
	public Transform hutdestroyparticle1 ; 
	public Transform streetlightdestroyparticle ;
	public Transform ArrowDestroy ;
	public Transform DestroyarrowGirl;
	public GameObject Snakedestroy;
	public Transform footparticle;

	public GameObject FailPanel;
	public GameObject PausePannel;
	public GameObject soundplay;
	public GameObject soundmute;
	public GameObject PauseButton;
	public GameObject bonousText;
	public GameObject PowerCountImages;

	public  Text height;
	public  Text BestHeight;
	public  Text pauseText;
	public  Text PowerCountText;
	private Animation destroyspr;
	private int powerCount;

	// Use this for initialization
	void Start () {

		/*.................Add Calling function store reward nd full add...............*/
		if ((Test.FailAdStartday==0 || (Test.FailAdStartday >= Test.installationDays)) && isFailrequest(Test.FailAdOccurance)) Test.adsCalling (Test.FailAdType, Test.FailAdId);
		if ((Test.PauseAdStartday == 0 || (Test.PauseAdStartday >= Test.installationDays)) && isPauserequest(Test.PauseAdOccurance)) Test.adsCalling (Test.PauseAdType, Test.PauseAdId);

		MyConstant.getlevel1 (MyConstant.Level1);
		speed = MyConstant.modelmovingspeed;
		powspeed = speed - 2;
		change = false;
		TouchDown = false;
		PowerMode = false;
		anim = GetComponent<Animator>();
		position = transform.position;
		anim.speed = .8f;
		powerCount = 0;
		bgsound.loop = true;
		bgsound.volume = .5f;
		bgsound.Play ();


     	SoundManagerScript.run.clip = SoundManagerScript.sound [3];
   		SoundManagerScript.run.Play ();

	}

	IEnumerator FinishedPowerText(){
		yield return new WaitForSeconds (.65f);
		bonousText.SetActive (false);

	}

	IEnumerator FinishedNinjLegParticles(){
		yield return new WaitForSeconds (2.5f);
		footparticle.GetComponent<ParticleSystem> ().gameObject.SetActive (false);

	}

	void OnTriggerEnter2D(Collider2D other){
	//	Debug.Log ("trigger11111.."+TouchDown+"  "+MyConstant.bird_point+"  "+other.gameObject.name+"  "+anim.GetBool("isJump")+"  "+MyConstant.gamePlay+"  "+PowerMode);

		if (anim.GetBool ("isJump") == true && other.gameObject.name.Equals ("Bird") == true) {
			//Debug.Log ("birdd.."+other.transform.position);
			Vector3 v = other.transform.position;
			v.x+= 5f;
			//other.transform.position += new Vector3 (1.8f,0,0);
			//Debug.Log ("birdd111.."+other.transform.position);

			other.GetComponent<Animator> ().SetBool ("flyDestroy", true);
			other.GetComponent<Animator> ().transform.localScale = new Vector3 (1.8f, 1.8f, 1.8f);
			other.GetComponent<Animator> ().transform.position = v + new Vector3 (0,3f,0);
			other.GetComponent<Collider2D> ().enabled = false;


			StartCoroutine (FinishedPowerText ());
			bonousText.transform.position = new Vector3 (600, 600, 600);
			bonousText.SetActive (true);
			Bg_ScrollingController.bonous = 500;

			if (footparticle.GetComponent<ParticleSystem> ().gameObject.activeSelf == false) {
				footparticle.GetComponent<ParticleSystem> ().gameObject.transform.position = anim.gameObject.transform.position;
				footparticle.GetComponent<ParticleSystem> ().gameObject.SetActive (true);	
				footparticle.GetComponent<ParticleSystem> ().Play ();
				StartCoroutine (FinishedNinjLegParticles ());
			} 
		} 
		else if (anim.GetBool ("isJump") == true && other.gameObject.name.Equals ("Stone") == true) {
			particle.GetComponent<ParticleSystem> ().gameObject.transform.position = other.gameObject.transform.position;
			particle.GetComponent<ParticleSystem> ().gameObject.SetActive(true);	
			particle.GetComponent<ParticleSystem> ().Play();
			StartCoroutine (FinishedPowerText ());
			bonousText.transform.position = new Vector3 (600, 600, 600);
			bonousText.SetActive (true);
			Bg_ScrollingController.bonous = 500;
			Destroy (other.gameObject);

			if (footparticle.GetComponent<ParticleSystem> ().gameObject.activeSelf == false) {
				footparticle.GetComponent<ParticleSystem> ().gameObject.transform.position = anim.gameObject.transform.position;
				footparticle.GetComponent<ParticleSystem> ().gameObject.SetActive (true);	
				footparticle.GetComponent<ParticleSystem> ().Play ();
				StartCoroutine (FinishedNinjLegParticles ());
			} 
		}
		else if (MyConstant.gamePlay) {
			if (other.gameObject.name.Equals ("Glowing_Circle_0") == false && PowerMode == false) {

				if (other.gameObject.name.Equals ("GameObject") == true) {
					ArrowDestroy.GetComponent<ParticleSystem> ().gameObject.transform.position = other.gameObject.transform.position;
					ArrowDestroy.GetComponent<ParticleSystem> ().gameObject.SetActive (true);	
					ArrowDestroy.GetComponent<ParticleSystem> ().Play ();
					Destroy (other.gameObject);}

				MyConstant.gamePlay = false;
				MyConstant.falling = false;
				MyConstant.fall = 0;

				bgsound.Stop ();
				SoundManagerScript.run.Stop ();
				SoundManagerScript.run.clip = SoundManagerScript.sound [0];
				SoundManagerScript.run.Play ();

				StartCoroutine (Falling_Failed ());

			} else if (PowerMode == true ) {

				if (other.gameObject.name.Equals ("Glowing_Circle_0") == true) starpower (other.gameObject);
				else if ( MyConstant.gamePlay == true && PowerMode == true) {
					starpower (other.gameObject);
					if(powerCount ==0)
					PowerMode = false;
				}
			}
			else {
				Destroy (other.gameObject);
				starpower (other.gameObject);
				PowerMode = true;

				SoundManagerScript.run.Stop ();
				SoundManagerScript.run.clip = SoundManagerScript.sound [1];
				SoundManagerScript.run.Play ();
				StartCoroutine (runAfterPowerSound ());
			}
		}
	}

	IEnumerator runAfterPowerSound(){
		yield return new WaitForSeconds (.5f);
		SoundManagerScript.run.Stop ();
		SoundManagerScript.run.clip = SoundManagerScript.sound [3];
		SoundManagerScript.run.Play ();
	}

	public void starpower( GameObject other){
	//	Debug.Log ("powerrrrrmodee,,,," + other.gameObject.name);

		if (other.gameObject.name.Equals ("Glowing_Circle_0") == true) {

			SoundManagerScript.run.Stop ();
			SoundManagerScript.run.clip = SoundManagerScript.sound [1];
			SoundManagerScript.run.Play ();
			StartCoroutine (runAfterPowerSound ());

			powerCount++;
			if (powerCount > 0 && PowerCountImages.activeSelf == false)
				PowerCountImages.SetActive (true);

			if (powerCount > 0) {
				PowerCountText.text =  powerCount.ToString ();
			}
			if (other.gameObject.name.Equals ("Bird") == false)
				Destroy (other.gameObject);
			
			starpowerparticle.GetComponent<ParticleSystem> ().gameObject.transform.position = other.gameObject.transform.position;
			starpowerparticle.GetComponent<ParticleSystem> ().gameObject.SetActive (true);	
			starpowerparticle.GetComponent<ParticleSystem> ().Play ();
		} 
		else {
			
			if (other.gameObject.name.Equals ("Hut_light_Sprite_0") == true) {
				hutdestroyparticle.GetComponent<ParticleSystem> ().gameObject.transform.position = other.gameObject.transform.position;
				hutdestroyparticle.GetComponent<ParticleSystem> ().gameObject.SetActive (true);	
				hutdestroyparticle.GetComponent<ParticleSystem> ().Play ();
			} else if (other.gameObject.name.Equals ("Hut_light_Sprite_1") == true) {
				hutdestroyparticle1.GetComponent<ParticleSystem> ().gameObject.transform.position = other.gameObject.transform.position;
				hutdestroyparticle1.GetComponent<ParticleSystem> ().gameObject.SetActive (true);	
				hutdestroyparticle1.GetComponent<ParticleSystem> ().Play ();
			} else if (other.gameObject.name.Equals ("Beam_01") == true) {
				streetlightdestroyparticle.GetComponent<ParticleSystem> ().gameObject.transform.position = other.gameObject.transform.position;
				streetlightdestroyparticle.GetComponent<ParticleSystem> ().gameObject.SetActive (true);	
				streetlightdestroyparticle.GetComponent<ParticleSystem> ().Play ();
			} else if (other.gameObject.name.Equals ("Street-Lyt-sprite-sheet_01_0") == true) {
				streetlightdestroyparticle.GetComponent<ParticleSystem> ().gameObject.transform.position = other.gameObject.transform.position;
				streetlightdestroyparticle.GetComponent<ParticleSystem> ().gameObject.SetActive (true);	
				streetlightdestroyparticle.GetComponent<ParticleSystem> ().Play ();
			} else if (other.gameObject.name.Equals ("Bird") == true) {
				other.GetComponent<Animator> ().SetBool ("flyDestroy", true);
				other.GetComponent<Animator> ().transform.localScale = new Vector3 (2.5f, 2.5f, 1f);
			} else if (other.gameObject.name.Equals ("GameObject") == true) {
				ArrowDestroy.GetComponent<ParticleSystem> ().gameObject.transform.position = other.gameObject.transform.position;
				ArrowDestroy.GetComponent<ParticleSystem> ().gameObject.SetActive (true);	
				ArrowDestroy.GetComponent<ParticleSystem> ().Play ();
			} else if (other.gameObject.name.Equals ("Oldman_01") == true) {
				particle.GetComponent<ParticleSystem> ().gameObject.transform.position = other.gameObject.transform.position;
				particle.GetComponent<ParticleSystem> ().gameObject.SetActive (true);	
				particle.GetComponent<ParticleSystem> ().Play ();
			} else if (other.gameObject.name.Equals ("Snake_Sprite_01_0") == true) {
				Snakedestroy.GetComponent<ParticleSystem> ().gameObject.transform.position = other.gameObject.transform.position;
				Snakedestroy.GetComponent<ParticleSystem> ().gameObject.SetActive (true);	
				Snakedestroy.GetComponent<ParticleSystem> ().Play ();
			} else if (other.gameObject.name.Equals ("Zin") == true) {
				DestroyarrowGirl.GetComponent<ParticleSystem> ().gameObject.transform.position = other.gameObject.transform.position;
				DestroyarrowGirl.GetComponent<ParticleSystem> ().gameObject.SetActive (true);	
				DestroyarrowGirl.GetComponent<ParticleSystem> ().Play ();
			} else if (other.gameObject.name.Equals ("Stone") == true) {
				particle.GetComponent<ParticleSystem> ().gameObject.transform.position = other.gameObject.transform.position;
				particle.GetComponent<ParticleSystem> ().gameObject.SetActive (true);	
				particle.GetComponent<ParticleSystem> ().Play ();
			} else if (other.gameObject.name.Equals ("LadyBug-Sprite 1_0") == true) {
				insectparticle.GetComponent<ParticleSystem> ().gameObject.transform.position = other.gameObject.transform.position;
				insectparticle.GetComponent<ParticleSystem> ().gameObject.SetActive (true);	
				insectparticle.GetComponent<ParticleSystem> ().Play ();
			} else if (other.gameObject.name.Equals ("Warrior-Run_0") == true) {
				insectparticle.GetComponent<ParticleSystem> ().gameObject.transform.position = other.gameObject.transform.position;
				insectparticle.GetComponent<ParticleSystem> ().gameObject.SetActive (true);	
				insectparticle.GetComponent<ParticleSystem> ().Play ();
			} else {
				other.GetComponent<Collider2D> ().enabled = false;
			}
			if (other.gameObject.name.Equals ("Bird") == false)
				Destroy (other.gameObject);
			
			powerCount--;
			if (powerCount < 1) {
				powerCount = 0;
				PowerCountText.text = " " ;
				PowerCountImages.SetActive (false);
			}else
			PowerCountText.text = powerCount.ToString ();
		
		}
	}

	void OnTriggerExit2D(Collider2D other){
	}

	IEnumerator Falling_Failed(){
		
		footparticle.GetComponent<ParticleSystem> ().gameObject.SetActive (false);
		yield return new WaitForSeconds(1.5f);
		MyConstant.falling = true;
		yield return new WaitForSeconds(.01f);
		Time.timeScale = 0;
		height.text = Bg_ScrollingController.count.ToString();
		if( PlayerPrefs.GetInt ("bestHeight")< Bg_ScrollingController.count)  PlayerPrefs.SetInt ("bestHeight", Bg_ScrollingController.count);
		BestHeight.text = PlayerPrefs.GetInt ("bestHeight").ToString ();
		SoundManagerScript.run.Stop ();
		FailPanel.gameObject.SetActive(true);
		PauseButton.SetActive (false);
	//	Generate.AfterLusthGenerate = true;

		if ((Test.FailAdStartday==0 ||(Test.FailAdStartday <= Test.installationDays)) && isFailDisplay(Test.FailAdOccurance)) Test.adsCallingrewardFull (Test.FailAdType, Test.FailAdId);

	}

	// Update is called once per frame
	void Update () {
		if (PowerMode == false && animpower.activeSelf == true) {
			animpower.SetActive (false);
		}else if(PowerMode == true && animpower.activeSelf == false){
			animpower.transform.localScale = new Vector3 (.83f, 1f, 1f);
			animpower.SetActive (true);
		}

		if (MyConstant.gamePlay == false) {
			Falling_Position ();
		}

		if (TouchDown && MyConstant.gamePlay) {
			NinjaTouchAnimation ();
		}

		if ( TouchDown==false && (Input.GetMouseButtonDown (0)&& (Input.mousePosition.y>90||(Input.mousePosition.y<90 && Input.mousePosition.x<410)))&& PausePannel.activeSelf == false && FailPanel.activeSelf==false && MyConstant.gamePlay && anim.speed>=1.2f) {
			NinjaTouchDown ();
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			
			if (FailPanel.activeSelf == true) {
				if (Test.GameAdStartday==0||(Test.GameAdStartday >= Test.installationDays)) Test.adsDestroy(Test.GameAdType);

				Time.timeScale = 0;
				SceneManager.LoadScene (0);
				Time.timeScale = 1;
			} else if (PausePannel.activeSelf == true) {
				
				SoundManagerScript.run.Play();
				bgsound.Play ();

				PausePannel.SetActive (false);
				Time.timeScale = 1;
				PauseAndResume.pause = false;
				PauseButton.SetActive (true);
			}
			else{
				if ((Test.PauseAdStartday==0 ||(Test.PauseAdStartday >= Test.installationDays)) && isPauseDisplay(Test.PauseAdOccurance)) Test.adsCallingrewardFull (Test.PauseAdType, Test.PauseAdId);
				if ((Test.PauseAdStartday==0 ||(Test.PauseAdStartday >= Test.installationDays)) && isPauserequest(Test.PauseAdOccurance) ) Test.adsCalling (Test.PauseAdType, Test.PauseAdId);

			SoundManagerScript.run.Stop();
		    bgsound.Stop ();
			Time.timeScale = 0;
			PauseButton.SetActive (false);
			pauseText.text = Bg_ScrollingController.count.ToString ();
			PauseAndResume.pause = !PauseAndResume.pause;
			if (PauseAndResume.pause) {
				if (PlayerPrefs.GetInt ("sound") == 1) {
					soundmute.SetActive (false);
					soundplay.SetActive (true);
				} else if (PlayerPrefs.GetInt ("sound") == 2) {
					soundplay.SetActive (false);
					soundmute.SetActive (true);
				}
				
				PausePannel.SetActive (true);
			}
		  }
		}
	}

	void NinjaTouchDown(){
		TouchDown= true;

		if (change == false) {
			transform.position = new Vector3 (-2.4f, -2.76f, 52.4f);
		} else if (change == true) {
			transform.position = new Vector3 (2.3f, -2.76f, 52.4f);
		}
		transform.rotation = Quaternion.Euler (new Vector3 (0, 180, 0));

		anim.SetBool ("isJump" ,true);

		SoundManagerScript.run.Stop ();
		SoundManagerScript.run.clip = SoundManagerScript.sound [2];
		SoundManagerScript.run.Play ();
	}

	void NinjaTouchAnimation(){
		Vector3 movement = new Vector3 (1, 0, 0);
		Vector3 movement1 = new Vector3 (0, 1, 0);
		if (change==false && Vector3.Distance (transform.position, position) <= 4.5f) {
	        transform.Translate (movement * -speed * Time.deltaTime);
			animpower.transform.Translate (movement * powspeed * Time.deltaTime);
			colisiongameObject.transform.Translate (movement * (powspeed-.5f)* Time.deltaTime);

			Vector3 v = transform.position;
			Vector3 v1 = animpower.transform.position;
			Vector3 v2 = colisiongameObject.transform.position;

			//Debug.Log ("position y.." + v.y+"  "+JumpTop);
			if (v.y <= -2.46 && JumpTop == false) {
				v.y += .03f;
				v1.y += .03f;
				v2.y+=.03f;
			} else if (JumpTop == true && v.y >= -2.76) {
				v.y -= .03f;
				v1.y -= .03f;
				v2.y-=.03f;
			}
			transform.position = v;
			animpower.transform.position = v1;
			colisiongameObject.transform.position = v2;
		
			if (footparticle.GetComponent<ParticleSystem> ().gameObject.activeSelf == true)  
				footparticle.GetComponent<ParticleSystem> ().gameObject.transform.position = anim.gameObject.transform.position;
		} else if (change==true &&  transform.position.x>=-1.9) {
			transform.Translate (movement * speed * Time.deltaTime); 
			animpower.transform.Translate (movement * -powspeed * Time.deltaTime);
			colisiongameObject.transform.Translate (movement * -(powspeed-.5f) * Time.deltaTime);

			Vector3 v = transform.position;
			Vector3 v1 = animpower.transform.position;
			Vector3 v2 = colisiongameObject.transform.position;

			//Debug.Log ("position y.." + v.y+"  "+JumpTop);
			if (v.y <= -2.46 && JumpTop == false) {
				v.y += .03f;
				v1.y += .03f;
					v2.y+=.03f;
			} else if (JumpTop == true && v.y >= -2.76) {
				v.y -= .03f;
				v1.y -= .03f;
				v2.y-=.03f;
			}
			transform.position = v;
			animpower.transform.position = v1;
			colisiongameObject.transform.position = v2;
			if (footparticle.GetComponent<ParticleSystem> ().gameObject.activeSelf == true)  
				footparticle.GetComponent<ParticleSystem> ().gameObject.transform.position = anim.gameObject.transform.position;
		}
		else {
			TouchDown = false;
			JumpTop = false;
			SoundManagerScript.run.Stop ();
			SoundManagerScript.run.clip = SoundManagerScript.sound [3];
			SoundManagerScript.run.Play ();

			if(change==false){
				change = true;
				anim.transform.position = new Vector3 (2.4f, -2.75f, 52.4f);
				if (footparticle.GetComponent<ParticleSystem> ().gameObject.activeSelf == true)  
					footparticle.GetComponent<ParticleSystem> ().gameObject.transform.position = anim.gameObject.transform.position;
				animpower.transform.position = new Vector3 (1.8f,-2.3f,52f);
				colisiongameObject.transform.position = new Vector3 (1.9f,-2.3f,52.4f);
				transform.rotation = Quaternion.Euler (new Vector3 (-88.8620f, -1.277f, 91.2780f));
			}
			else if(change==true){
				change = false;
				anim.transform.position = new Vector3 (-2.4f, -2.75f, 52.4f);
				if (footparticle.GetComponent<ParticleSystem> ().gameObject.activeSelf == true)  
					footparticle.GetComponent<ParticleSystem> ().gameObject.transform.position = anim.gameObject.transform.position;
				animpower.transform.position = new Vector3 (-1.8f,-2.2f,52f);
				colisiongameObject.transform.position = new Vector3 (-1.85f,-2.3f,52.4f);
				transform.rotation = Quaternion.Euler (new Vector3 (-87.291f, -74.215f, -15.768f));
			}
			anim.SetBool ("isJump" ,false);
		}
	}

	void Falling_Position(){

		if (change == false) {
			transform.rotation = Quaternion.Euler (new Vector3 (0, -74.215f, -15.768f));
			anim.SetBool ("isFalling", true);
			Vector3 v = anim.transform.position;

			if (MyConstant.falling == true) {
				v.y -= .08f;
			}
			else if (v.x <= -.4f) {
				v.x += .06f;
				v.y -= .08f;
				MyConstant.fall++;
			} else if(MyConstant.fall >= -2 &&MyConstant.fall <= 0 ){//Debug.Log(".......4444");
				v.y -=.8f;
				MyConstant.fall -= 1;
			}

			transform.position = v;
		} else if (change == true) {
			transform.rotation = Quaternion.Euler (new Vector3 (0, 85.90701f, 0));
			anim.SetBool ("isFalling", true);
			Vector3 v = anim.transform.position;

			if (MyConstant.falling == true) v.y -= .08f;
			else if (v.x >= .45f) {
				v.x -= .08f;
				v.y -= .08f;
				MyConstant.fall++;
			} 
			else if(MyConstant.fall >= -2 &&MyConstant.fall <= 0){
				v.y -=.8f;
				MyConstant.fall -= 1;
			}
			transform.position = v;
		}
	}

	public bool isFailrequest(int server_val){
		int local_val = PlayerPrefs.GetInt ("FailOccurLocal");;
		local_val++;
		if(server_val == 0)
			return true;
		if (local_val % server_val == 0)
				return true;
			else
				return false;
		
	}
	public bool isFailDisplay(int server_val){
		int local_val = PlayerPrefs.GetInt ("FailOccurLocal");
		local_val++;
		if(server_val == 0)
			return true;
		if(server_val == local_val)
			PlayerPrefs.SetInt("FailOccurLocal",  0) ;
		else
			PlayerPrefs.SetInt("FailOccurLocal",  local_val) ;

		if (local_val % server_val == 0)
			return true;
		else
			return false;
	
	}

	public static bool isPauserequest(int server_val){
		int local_val = PlayerPrefs.GetInt ("PauseOccurLocal");;
		local_val++;
		if(server_val == 0)
			return true;
		if (local_val % server_val == 0)
			return true;
		else
			return false;

	}
	public static  bool isPauseDisplay(int server_val){
		int local_val = PlayerPrefs.GetInt ("PauseOccurLocal");
		local_val++;
		if(server_val == 0)
			return true;
		if(server_val == local_val)
			PlayerPrefs.SetInt("PauseOccurLocal",  0) ;
		else
			PlayerPrefs.SetInt("PauseOccurLocal",  local_val) ;

		if (local_val % server_val == 0)
			return true;
		else
			return false;

	}


}
