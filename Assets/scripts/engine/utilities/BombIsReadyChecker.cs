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
				bomb_cooldown.Reset();

      game_event_listener.DeleteNewestMessage();
    }

    if (Input.GetKey(KeyCode.L))
    {
      Debug.Log("bomb_cooldown.IsExpired(): " + bomb_cooldown.IsExpired());
      Debug.Log("MaxBombsNotExceeded(): " + MaxBombsNotExceeded());
      Debug.Log("LevelNotCompleted(): " + LevelNotCompleted());
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
    return BombScript.BombCount < max_bombs_upgrader.GetValue();
  }
}