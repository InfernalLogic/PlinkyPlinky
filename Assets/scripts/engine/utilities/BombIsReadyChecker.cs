using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Timer))]
public class BombIsReadyChecker : Singleton<BombIsReadyChecker>
{
  [SerializeField]
  private Timer bomb_cooldown;

  private MaxBombsUpgrader max_bombs_upgrader;

  private Subscriber<GameEvent> game_event_listener = new Subscriber<GameEvent>();

  new void Awake()
  {
    base.Awake();
    max_bombs_upgrader = FindObjectOfType<MaxBombsUpgrader>();
    bomb_cooldown = GetComponentInChildren<Timer>();
  }

  void Start()
  {
    GameEvents.AddSubscriber(game_event_listener);
  }

  void Update()
  {
    while (!game_event_listener.IsEmpty())
    {
      if (game_event_listener.ReadNewestMessage() == GameEvents.bomb_dropped_event)
      {
				bomb_cooldown.Reset();
        game_event_listener.DeleteNewestMessage();
      }
      else
        game_event_listener.DeleteNewestMessage();
    }
  }

  public bool BombIsReady()
  {
    return (bomb_cooldown.IsExpired() && MaxBombsNotExceeded() && LevelNotCompleted());
  }

  private bool LevelNotCompleted()
  {
    return !LevelCompleteChecker.Instance.IsLevelCompleted();
  }

  private bool MaxBombsNotExceeded()
  {
    return BombScript.GetBombCount() < max_bombs_upgrader.GetValue();
  }
}