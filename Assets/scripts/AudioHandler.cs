using UnityEngine;
using System.Collections;

public class AudioHandler : MonoBehaviour 
{
	public AudioClip[] pop_sounds;
	public AudioClip bumper_hit_sound;

	public AudioClip GetPopSound()
	{
		Debug.Log ("length: " + pop_sounds.Length);
		int sound = Random.Range(0, (pop_sounds.Length));
		Debug.Log ("element number: " + sound);
		return pop_sounds[sound];
	}

	public AudioClip GetBumperHitSound()
	{
		return bumper_hit_sound;
	}

}
