using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Timer))]
public class BombIsReadyChecker : Singleton<BombIsReadyChecker>
{
  [SerializeField]
  private Timer bomb_cooldown;

  private GameObject selected_bomb;
  private BombScript selected_bomb_script;

  private MaxBombsUpgrader max_bombs_upgrader;
  private BombCooldownUpgrader bomb_cooldown_upgrader;

  private Subscriber<GameEvent> game_event_listener = new Subscriber<GameEvent>();

  void Awake()
  {
    selected_bomb = FindObjectOfType<Plinker>().selected_bomb;
    selected_bomb_script = selected_bomb.GetComponent<BombScript>();
    bomb_cooldown_upgrader = FindObjectOfType<BombCooldownUpgrader>();
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
    return !LevelCompleteChecker.Instance().IsLevelCompleted();
  }

  private bool MaxBombsNotExceeded()
  {
    return BombScript.GetBombCount() < max_bombs_upgrader.GetValue();
  }
}