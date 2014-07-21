using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEventRegistry : Singleton<GameEventRegistry> 
{
  private Dictionary<string, GameEvent> game_events = new Dictionary<string, GameEvent>();

  new void Awake()
  {
    base.Awake();

    GameEvent[] events = GetComponentsInChildren<GameEvent>();

    foreach (GameEvent game_event in events)
    {
      game_events.Add(game_event.name, game_event);
    }
  }

  public GameEvent FindEventByName(string name)
  {
    if (game_events.ContainsKey(name))
      return game_events[name];
    else
    {
      Debug.LogException(new System.Exception("FindEventByName(" + name + ") failed!"));
      return null;
    }

  }
}
