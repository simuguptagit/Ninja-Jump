using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {

	public static AudioClip[] sound;

	public static AudioSource fail;
	public static AudioSource jump;
	public static AudioSource power;
	public static AudioSource run;
	// Use this for initialization
	void Start () {
		/*..........................sound................*/
		sound = Resources.LoadAll<AudioClip>("Music");

		AudioSource newAudio =  gameObject.GetComponent<AudioSource> ();
		fail = AddAudioSource (newAudio,sound[0], false , true, 1f);

		AudioSource newAudio1 =  gameObject.GetComponent<AudioSource> ();
		power = AddAudioSource (newAudio1,sound[1], false , true, 1f);

		AudioSource newAudio2 =  gameObject.GetComponent<AudioSource> ();
		jump = AddAudioSource (newAudio2,sound[2], false , true, 1f);

		AudioSource newAudio3 =  gameObject.GetComponent<AudioSource> ();
		run = AddAudioSource (newAudio3,sound[3], true , true, .5f);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static AudioSource AddAudioSource (AudioSource newAudio,AudioClip clip , bool loop, bool awake , float vol){
		newAudio.clip = clip;
		newAudio.loop = loop;
		newAudio.playOnAwake = awake;
		newAudio.volume = vol;
		return newAudio;
	}
}
