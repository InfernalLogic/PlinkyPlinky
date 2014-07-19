using UnityEngine;
using System.Collections;

public class AudioHandler : Singleton<AudioHandler> 
{
	public AudioClip[] pop_sounds;
	public AudioClip[] bumper_hit_sounds;
	public AudioClip bomb_drop_sound;
	public AudioClip coin_hit_sound;

	public AudioClip GetPopSound()
	{
    return RandomSoundFrom(pop_sounds);
	}

	public AudioClip GetBumperHitSound()
	{
    return RandomSoundFrom(bumper_hit_sounds);
	}

	public AudioClip GetBombDropSound()
	{
		return bomb_drop_sound;
	}

	public AudioClip GetCoinHitSound()
	{
		return coin_hit_sound;
	}

  private AudioClip RandomSoundFrom(AudioClip[] clips)
  {
    int sound = Random.Range(0, (clips.Length));
    return clips[sound];
  }
}
