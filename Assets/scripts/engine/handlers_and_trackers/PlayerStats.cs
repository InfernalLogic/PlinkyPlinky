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

  private IDictionary<string, ScoringObject> scoring_objects = new Dictionary<string, ScoringObject>();

  private Subscriber<GameEvent> game_event_listener = new Subscriber<GameEvent>();

  new void Awake()
  {
    base.Awake();
    GameEventPublisher.Instance().AddSubscriber(game_event_listener);

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
      if (scoring_objects.ContainsKey(game_event_listener.ReadNewestMessage().name))
      {
        AddMoney(scoring_objects[game_event_listener.ReadNewestMessage().name].GetPointValue());
        game_event_listener.DeleteNewestMessage();
      }
      else
      {
        game_event_listener.DeleteNewestMessage();
      }
    }
  }

  private void InitializeScoringObjectsDictionary()
  {
    ScoringObject[] scoring_objects_in_children = GetComponentsInChildren<ScoringObject>();

    foreach (ScoringObject scoring_object in scoring_objects_in_children)
    {
      scoring_objects.Add(scoring_object.GetRelevantEventName(), scoring_object);
    }

    Debug.Log("Added " + scoring_objects.Count + " scoring_objects to player_stats");
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

    UpgradeableObject[] upgrades = Resources.FindObjectsOfTypeAll<UpgradeableObject>();

    foreach (UpgradeableObject upgrade in upgrades)
    {
      upgrade.RecalculateUpgradeCost();
    }

    ScoringObject[] scoring_objects = Resources.FindObjectsOfTypeAll<ScoringObject>();

    foreach (ScoringObject scoring_object in scoring_objects)
    {
      scoring_object.RecalculatePointValue();
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