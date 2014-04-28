using UnityEngine;
using System.Collections;

public class AudioHandler : MonoBehaviour 
{
	public AudioClip[] pop_sounds;
	public AudioClip bumper_hit_sound;
	public AudioClip bomb_drop_sound;

	public AudioClip GetPopSound()
	{
		int sound = Random.Range(0, (pop_sounds.Length));
		return pop_sounds[sound];
	}

	public AudioClip GetBumperHitSound()
	{
		return bumper_hit_sound;
	}

	public AudioClip GetBombDropSound()
	{
		return bomb_drop_sound;
	}
}
