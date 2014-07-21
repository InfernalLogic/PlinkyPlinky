using UnityEngine;
using System.Collections;

public class LevelCompleteChecker : Singleton<LevelCompleteChecker> 
{
  [SerializeField]
	private float load_new_level_delay = 0f;

	private int coins_left = 0;
	private int bombs_dropped = 0;

	private float load_new_level_countdown;
	private bool level_completed = false;

  private Subscriber<GameEvent> coin_hit_subscriber = new Subscriber<GameEvent>();

  void Awake()
  {
    CoinScript.coin_hit_publisher.AddSubscriber(coin_hit_subscriber);
  }

	void Update()
	{
    if (!coin_hit_subscriber.IsEmpty())
    {
      CoinHit();
      coin_hit_subscriber.DeleteNewestMessage();
    }

    if (AllCoinsHit())
		{
      if (!level_completed)
      {
        level_completed = true;
        Debug.Log("Level completed with " + bombs_dropped + " bombs dropped");

        ResetNewLevelCountdown();
      } 
      else if (level_completed && NewLevelCountdownCooledDown())
			{
        LoadNewLevel();
        level_completed = false;
        CountCoins();
        bombs_dropped = 0;
			}
		}
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

	public void CoinHit()
	{
    //the coins must be counted, then decremented here for anything to behave properly. There's what I believe is called
    //a "race hazard" having to do with how objects are destroyed and when update() is called, causing a call to CountCoins()
    //performed after a new level load to include any coins that had not been previously destroyed.
    CountCoins();
    --coins_left;
	}

	public void BombDropped()
	{
		++bombs_dropped;
	}

	public void CountCoins()
	{
		GameObject[] goal_counter = GameObject.FindGameObjectsWithTag("goal");
		coins_left = goal_counter.Length;
	}
	
	public void SetCoinsLeftToZero()
	{
		coins_left = 0;
	}
}
