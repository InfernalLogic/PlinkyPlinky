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
    GameEventPublisher.Instance().AddSubscriber(game_event_subscriber);
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
        level_completed = true;
        Debug.Log("Level completed with " + bombs_dropped + " bombs dropped");

        ResetNewLevelCountdown();
      } 
      else if (level_completed && NewLevelCountdownCooledDown() && BombScript.GetBombCount() <= 0)
			{
        LoadNewLevel();
        level_completed = false;
        CountCoins();
        bombs_dropped = 0;
			}
		}
	}

  private void HandleMessage(GameEvent message)
  {
    if (message == GameEventPublisher.Instance().bomb_dropped_event)
      ++bombs_dropped;
    else if (message == GameEventPublisher.Instance().coin_hit_event)
      RegisterCoinHit();
  }

  private void ResetNewLevelCountdown()
  {
    load_new_level_countdown = Time.time + load_new_level_delay;
  }

  private void LoadNewLevel()
  {
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

	public void RegisterCoinHit()
	{
    //the coins must be counted, here for anything to behave properly. There's what I believe is called
    //a "race hazard" having to do with how objects are destroyed and when update() is called, causing a call to CountCoins()
    //performed after a new level load to include any coins that had not been previously destroyed.
    CountCoins();
    if (coins_left == 0 && bombs_dropped > 0)
      GameEventPublisher.Instance().PublishMessage(GameEventPublisher.Instance().level_completed_event);
	}

	public void CountCoins()
	{
		GameObject[] goal_counter = GameObject.FindGameObjectsWithTag("goal");
		coins_left = goal_counter.Length;
    Debug.Log("Counted: " + coins_left);
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
