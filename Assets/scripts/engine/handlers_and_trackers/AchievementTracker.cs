using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementTracker : Singleton<AchievementTracker>
{
  public Subscriber<GameEvent> game_event_listener = new Subscriber<GameEvent>();

  private IDictionary<GameEvent, AchievementChain> achievement_chains = new Dictionary<GameEvent, AchievementChain>();

  new void Awake()
  {
    base.Awake();

    InitializeAchievementChainsDictionary();
    SubscribeToBombDroppedEvents();
    SubscribeToObjectHitEvents();
  }

  private void InitializeAchievementChainsDictionary()
  {
    AchievementChain[] achievement_chains_found = GetComponentsInChildren<AchievementChain>();

    foreach (AchievementChain chain in achievement_chains_found)
    {
      achievement_chains.Add(chain.GetRelevantEvent(), chain);
    }
  }

  void Update()
  {
    while (!game_event_listener.IsEmpty())
    {
      IncrementAchievementStat(game_event_listener.ReadNewestMessage());
      game_event_listener.DeleteNewestMessage();
    }
  }

  private void SubscribeToBombDroppedEvents()
  {
    BombScript.bomb_dropped_publisher.AddSubscriber(game_event_listener);
  }

  private void SubscribeToObjectHitEvents()
  {
    BumperScript.bumper_hit_publisher.AddSubscriber(game_event_listener);
    PegScript.peg_hit_publisher.AddSubscriber(game_event_listener);
    CoinScript.coin_hit_publisher.AddSubscriber(game_event_listener);
  }

  public void IncrementAchievementStat(GameEvent relevant_event)
  {
    if (achievement_chains.ContainsKey(relevant_event))
    {
      achievement_chains[relevant_event].Increment();
    }
    else
    {
      Debug.Log("IncrementAchievementStat could not find: " + relevant_event.name);
    }
  }

  public void EnqueueAchievementPopup(Texture popup_texture)
  {
    FindObjectOfType<AchievementUnlockedPopups>().EnqueueAchievementPopup(popup_texture);
  }
}
