using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementTracker : Singleton<AchievementTracker>
{
  public Subscriber<GameEvent> game_event_listener = new Subscriber<GameEvent>();

  private IDictionary<string, AchievementChain> achievement_chains = new Dictionary<string, AchievementChain>();

  new void Awake()
  {
    base.Awake();

    InitializeAchievementChainsDictionary();
    GameEvents.AddSubscriber(game_event_listener);
  }

  private void InitializeAchievementChainsDictionary()
  {
    AchievementChain[] achievement_chains_found = GetComponentsInChildren<AchievementChain>();

    foreach (AchievementChain chain in achievement_chains_found)
    {
      achievement_chains.Add(chain.GetRelevantEventName(), chain);
    }
  }

  void Update()
  {
    while (!game_event_listener.IsEmpty())
    {
      RouteGameEvent(game_event_listener.ReadNewestMessage());
      game_event_listener.DeleteNewestMessage();
    }
  }

  public void RouteGameEvent(GameEvent relevant_event)
  {
    if (achievement_chains.ContainsKey(relevant_event.name))
      achievement_chains[relevant_event.name].HandleGameEvent(relevant_event);
  }

  public AchievementChain FindAchievementChainFor(GameEvent game_event)
  {
    if (achievement_chains.ContainsKey(game_event.name))
      return achievement_chains[game_event.name];
    else
      return null;
  }
}
