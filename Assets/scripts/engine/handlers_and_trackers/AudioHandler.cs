using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioHandler : Singleton<AudioHandler> 
{
  public float default_music_volume = 0.75f;
  public SavedBool mute_sfx;
  public SavedBool mute_bgm;

  [SerializeField]
	private AudioClip[] peg_hit_sounds;
  [SerializeField]
  private AudioClip[] bumper_hit_sounds;
  [SerializeField]
  private AudioClip[] bomb_dropped_sounds;
  [SerializeField]
  private AudioClip[] coin_hit_sounds;
  [SerializeField]
  private AudioClip[] level_loaded_sounds;
  [SerializeField]
  private AudioClip[] level_completed_sounds;

  private Subscriber<GameEvent> game_event_listener = new Subscriber<GameEvent>();
  private Dictionary<string, AudioClip[]> audio_clips = new Dictionary<string, AudioClip[]>();

  private GameEvent current_event = null;

  void Start()
  {
    SubscribeToRelevantEvents();
    LoadAudioClipDictionary();
    InitializeBGM();

    print(mute_bgm.GetKey() + " initialized to " + mute_bgm.GetValue());
  }

  private void InitializeBGM()
  {
    if (mute_bgm.IsTrue())
      GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().volume = 0.0f;
    else
      GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().volume = default_music_volume;
  }

  void Update()
  {
    while (!game_event_listener.IsEmpty())
    {
      if (CurrentEventHasNotBeenPlayedThisUpdate())
      {
        current_event = game_event_listener.ReadNewestMessage();

        if (CurrentEventIsFound())
        {
          PlayEventSound(game_event_listener.ReadNewestMessage());
        }
      }
      game_event_listener.DeleteNewestMessage();
    }
    current_event = null;
  }

  private bool CurrentEventIsFound()
  {
    return audio_clips.ContainsKey(current_event.name);
  }

  private bool CurrentEventHasNotBeenPlayedThisUpdate()
  {
    return current_event != game_event_listener.ReadNewestMessage();
  }

  private void PlayEventSound(GameEvent game_event)
  {
    if (mute_sfx.IsFalse())
      AudioSource.PlayClipAtPoint(GetRandomSoundFrom(audio_clips[game_event.name]), Vector3.zero);
  }

  private void LoadAudioClipDictionary()
  {
    audio_clips.Add(GameEvents.peg_hit_event.name, peg_hit_sounds);
    audio_clips.Add(GameEvents.bumper_hit_event.name, bumper_hit_sounds);
    audio_clips.Add(GameEvents.coin_hit_event.name, coin_hit_sounds);
    audio_clips.Add(GameEvents.bomb_dropped_event.name, bomb_dropped_sounds);
    audio_clips.Add(GameEvents.level_loaded_event.name, level_loaded_sounds);
    audio_clips.Add(GameEvents.level_completed_event.name, level_completed_sounds);
  }

  private void SubscribeToRelevantEvents()
  {
    GameEvents.AddSubscriber(game_event_listener);
  }

  private AudioClip GetRandomSoundFrom(AudioClip[] clips)
  {
    int sound = Random.Range(0, (clips.Length - 1));
    return clips[sound];
  }

  public void MuteBGM()
  {
    GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().volume = 0.0f;

    mute_bgm.Set(true);
  }

  public void UnmuteBGM()
  {
    GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().volume = default_music_volume;

    mute_bgm.Set(false);
  }

  public void MuteSFX()
  {
    mute_sfx.Set(true);
  }

  public void UnmuteSFX()
  {
    mute_sfx.Set(false);
  }
}
