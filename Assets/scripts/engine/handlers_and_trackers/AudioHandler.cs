using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioHandler : Singleton<AudioHandler> 
{
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

  void Start()
  {
    SubscribeToRelevantEvents();
    LoadAudioClipDictionary();
  }

  void Update()
  {
    while (!game_event_listener.IsEmpty())
    {
      if (audio_clips.ContainsKey(game_event_listener.ReadNewestMessage()))
      {
        PlayEventSound(game_event_listener.ReadNewestMessage());
        game_event_listener.DeleteNewestMessage();
      }
      else
      {
        Debug.LogError("Could not find " + game_event_listener.ReadNewestMessage().name);
        game_event_listener.DeleteNewestMessage();
      }
    }
  }

  private void PlayEventSound(GameEvent game_event)
  {
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
}
