using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEventRegistry : Singleton<GameEventRegistry> 
{
  public static Dictionary<string, GameEvent> game_events = new Dictionary<string, GameEvent>();

  void Awake()
  {
    GameEvent[] events = GetComponentsInChildren<GameEvent>();

    foreach (GameEvent game_event in events)
    {
      game_events.Add(game_event.name, game_event);
    }
  }
}
