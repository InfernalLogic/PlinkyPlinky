﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementTracker : Singleton<AchievementTracker>
{
  public Subscriber<GameEvent> game_event_listener = new Subscriber<GameEvent>();

  private IDictionary<string, AchievementChain> achievement_chains;

  void Awake()
  {
    CheckForDuplicates();

    achievement_chains = new Dictionary<string, AchievementChain>();

    AchievementChain[] achievement_chains_found = GetComponentsInChildren<AchievementChain>();

    foreach (AchievementChain chain in achievement_chains_found)
    {
      achievement_chains.Add(chain.name, chain);
    }

    SubscribeToBombDroppedEvents();
    SubscribeToObjectHitEvents();
  }

  void Update()
  {
    while (!game_event_listener.IsEmpty())
    {
      Debug.Log(game_event_listener.ReadNewestMessage().name + " dequeued");
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

  public void IncrementAchievementStat(string stat_name)
  {
    if (achievement_chains.ContainsKey(stat_name))
    {
      achievement_chains[stat_name].Increment();
    }
    else
    {
      Debug.Log("IncrementAchievementStat could not find: " + stat_name);
    }
  }

  public void EnqueueAchievementPopup(Texture popup_texture)
  {
    FindObjectOfType<AchievementUnlockedPopups>().EnqueueAchievementPopup(popup_texture);
  }
}
