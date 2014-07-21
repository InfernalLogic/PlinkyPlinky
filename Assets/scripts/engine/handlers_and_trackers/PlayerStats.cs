using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : Singleton<PlayerStats> 
{
  [SerializeField]
  private bool reset_on_load_enabled = false;
  [SerializeField]
  private SavedStat current_money;
  [SerializeField]
  private SavedStat career_money;

	public BumperUpgrader bumper_upgrader;
	public PegUpgrader peg_upgrader;
	public CoinUpgrader coin_upgrader;
  public LevelUnlocker level_unlocker;

  private IDictionary<GameEvent, ScoringObject> scoring_objects = new Dictionary<GameEvent, ScoringObject>();

  private Subscriber<GameEvent> game_event_listener = new Subscriber<GameEvent>();

  new void Awake()
  {
    base.Awake();
    SubscribeToObjectHitEvents();

    if (reset_on_load_enabled)
    {
      PlayerPrefs.DeleteAll();
      ResetStats();
    }

    InitializeScoringObjectsDictionary();
  }

  void Update()
  {
    while (!game_event_listener.IsEmpty())
    {
      AddMoney(scoring_objects[game_event_listener.ReadNewestMessage()].GetPointValue());
      game_event_listener.DeleteNewestMessage();
    }
  }

  private void InitializeScoringObjectsDictionary()
  {
    ScoringObject[] scoring_objects_in_children = GetComponentsInChildren<ScoringObject>();

    foreach (ScoringObject scoring_object in scoring_objects_in_children)
    {
      scoring_objects.Add(scoring_object.GetRelevantEvent(), scoring_object);
    }

    Debug.Log("Added " + scoring_objects.Count + " scoring_objects to player_stats");
  }

  private void SubscribeToObjectHitEvents()
  {
    BumperScript.bumper_hit_publisher.AddSubscriber(game_event_listener);
    PegScript.peg_hit_publisher.AddSubscriber(game_event_listener);
    CoinScript.coin_hit_publisher.AddSubscriber(game_event_listener);
  }

	public void AddMoney(int income)
	{
		current_money.AddValue(income);
		career_money.AddValue(income);
	}

	public void ResetStats()
	{	
		SavedStat[] stats = Resources.FindObjectsOfTypeAll<SavedStat>();

    foreach (SavedStat element in stats)
		{
			element.Reset();
		}

    LevelHandler.Instance().LoadUnlockedLevels();
	
	}

	public void SpendMoney(int price)
	{
		current_money.AddValue(-price);
	}

	public int GetCurrentMoney()
	{
    if (current_money != null)
      return current_money.GetValue();
    else
      return -1;
	}
	
	public int GetCareerMoney()
	{
    return career_money.GetValue();
	}
}