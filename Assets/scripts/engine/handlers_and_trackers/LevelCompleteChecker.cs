using UnityEngine;
using System.Collections;

public class LevelCompleteChecker : Singleton<LevelCompleteChecker> 
{
  [SerializeField]
	private float load_new_level_delay = 0f;

	private int coins_left = 1;
	private int bombs_dropped = 0;

	private float load_new_level_countdown;
	private bool level_completed = false;

  private Subscriber<GameEvent> game_event_subscriber = new Subscriber<GameEvent>();
  
  void Awake()
  {
    GameEvents.AddSubscriber(game_event_subscriber);
  }

	void Update()
	{
    if (!game_event_subscriber.IsEmpty())
    {
      HandleMessage(game_event_subscriber.ReadNewestMessage());
      game_event_subscriber.DeleteNewestMessage();
    }

    if (AllCoinsHit())
		{
      if (!level_completed)
      {
        if (coins_left == 0 && bombs_dropped > 0)
          PublishLevelCompletedEvent();

        level_completed = true;
        Debug.Log("Level completed with " + bombs_dropped + " bombs dropped");

        ResetNewLevelCountdown();
      } 
      else if (level_completed && NewLevelCountdownCooledDown() && BombScript.GetBombCount() <= 0)
			{
        LoadNewLevel();
        CountCoins();
        bombs_dropped = 0;
			}
		}
	}

  private static void PublishLevelCompletedEvent()
  {
    GameEvents.Publish(GameEvents.level_completed_event);
  }

  private void HandleMessage(GameEvent message)
  {
    if (message == GameEvents.bomb_dropped_event)
      ++bombs_dropped;
    else if (message == GameEvents.coin_hit_event)
    {
      CountCoins();
    }
  }

  private void ResetNewLevelCountdown()
  {
    load_new_level_countdown = Time.time + load_new_level_delay;
  }

  private void LoadNewLevel()
  {
    level_completed = false;
    LevelHandler.Instance().LoadRandomLevel();
  }

  private bool NewLevelCountdownCooledDown()
  {
    return Time.time >= load_new_level_countdown;
  }

  private bool AllCoinsHit()
  {
    return coins_left <= 0;
  }

	public void CountCoins()
	{
		GameObject[] goal_counter = GameObject.FindGameObjectsWithTag("coin");
		coins_left = goal_counter.Length;
    Debug.Log("LevelCompleteChecker.CountCoins(): " + coins_left);
	}
	
	public void SetCoinsLeftToZero()
	{
		coins_left = 0;
	}

  public bool IsLevelCompleted()
  {
    return level_completed;
  }
}
