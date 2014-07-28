using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioHandler : Singleton<AudioHandler> 
{
  public bool mute_sound_effects = false;
  public bool mute_music = false;
  public float default_music_volume = 0.75f;

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
  private Dictionary<GameEvent, AudioClip[]> audio_clips = new Dictionary<GameEvent, AudioClip[]>();

  private GameEvent current_event = null;

  void Awake()
  {
    GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().volume = default_music_volume;
  }

  void Start()
  {
    SubscribeToRelevantEvents();
    LoadAudioClipDictionary();
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
    return audio_clips.ContainsKey(current_event);
  }

  private bool CurrentEventHasNotBeenPlayedThisUpdate()
  {
    return current_event != game_event_listener.ReadNewestMessage();
  }

  private void PlayEventSound(GameEvent game_event)
  {
    if (!mute_sound_effects)
      AudioSource.PlayClipAtPoint(GetRandomSoundFrom(audio_clips[game_event]), Vector3.zero);
  }

  private void LoadAudioClipDictionary()
  {
    audio_clips.Add(GameEventPublisher.Instance().peg_hit_event, peg_hit_sounds);
    audio_clips.Add(GameEventPublisher.Instance().bumper_hit_event, bumper_hit_sounds);
    audio_clips.Add(GameEventPublisher.Instance().coin_hit_event, coin_hit_sounds);
    audio_clips.Add(GameEventPublisher.Instance().bomb_dropped_event, bomb_dropped_sounds);
    audio_clips.Add(GameEventPublisher.Instance().level_loaded_event, level_loaded_sounds);
    audio_clips.Add(GameEventPublisher.Instance().level_completed_event, level_completed_sounds);
  }

  private void SubscribeToRelevantEvents()
  {
    GameEventPublisher.Instance().AddSubscriber(game_event_listener);
  }

  private AudioClip GetRandomSoundFrom(AudioClip[] clips)
  {
    int sound = Random.Range(0, (clips.Length - 1));
    return clips[sound];
  }

  public void MuteMusic()
  {
    GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().volume = 0.0f;

    mute_music = true;
  }

  public void UnmuteMusic()
  {
    GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().volume = default_music_volume;

    mute_music = false;
  }
}
