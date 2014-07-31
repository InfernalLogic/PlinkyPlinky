using UnityEngine;
using System.Collections;

public class BombIsReadyChecker : Singleton<BombIsReadyChecker>
{
  private GameObject selected_bomb;

  private MaxBombsUpgrader max_bombs_upgrader;
  private BombCooldownUpgrader bomb_cooldown_upgrader;
  private float bomb_cooldown_timer = 0f;
  private BombScript selected_bomb_script;

  private Subscriber<GameEvent> game_event_listener = new Subscriber<GameEvent>();

  void Awake()
  {
    selected_bomb = FindObjectOfType<Plinker>().selected_bomb;
    selected_bomb_script = selected_bomb.GetComponent<BombScript>();
    bomb_cooldown_upgrader = FindObjectOfType<BombCooldownUpgrader>();
    max_bombs_upgrader = FindObjectOfType<MaxBombsUpgrader>();
  }

  void Start()
  {
    GameEventPublisher.AddSubscriber(game_event_listener);
    bomb_cooldown_timer = Time.time;
  }

  void Update()
  {
    while (!game_event_listener.IsEmpty())
    {
      if (game_event_listener.ReadNewestMessage() == GameEventPublisher.bomb_dropped_event)
      {
        ResetBombCooldownTimer();
        game_event_listener.DeleteNewestMessage();
      }
      else
        game_event_listener.DeleteNewestMessage();
    }
  }

  public bool BombIsReady()
  {
    return BombIsCooledDown() && MaxBombsNotExceeded() && LevelNotCompleted();
  }

  private static bool LevelNotCompleted()
  {
    return !LevelCompleteChecker.Instance().IsLevelCompleted();
  }

	private bool BombIsCooledDown()
	{
    return (bomb_cooldown_timer <= Time.time);
	}

  private bool MaxBombsNotExceeded()
  {
    return BombScript.GetBombCount() < max_bombs_upgrader.GetValue();
  }

  private void ResetBombCooldownTimer()
  {
    bomb_cooldown_timer = Time.time + (BombScript.GetBaseCooldown() - ((float)bomb_cooldown_upgrader.GetValue() * 0.1f));
  }

}
