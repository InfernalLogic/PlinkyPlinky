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

  private IDictionary<string, ScoringObject> scoring_objects;

  new void Awake()
  {
    base.Awake();

    if (reset_on_load_enabled)
    {
      PlayerPrefs.DeleteAll();
      ResetStats();
    }

    scoring_objects = new Dictionary<string, ScoringObject>();
    ScoringObject[] scoring_objects_in_children = GetComponentsInChildren<ScoringObject>();

    foreach (ScoringObject scoring_object in scoring_objects_in_children)
    {
      scoring_objects.Add(scoring_object.name, scoring_object);
    }

    Debug.Log("Added " + scoring_objects.Count + " scoring_objects to player_stats");
  }

	void Start()
	{
		UpgradeableObject[] upgrades = Resources.FindObjectsOfTypeAll<UpgradeableObject>();

		foreach (UpgradeableObject element in upgrades)
		{
			element.RecalculateUpgradeCost();
		}
	}

	public void AddMoney(int income)
	{
		current_money.AddValue(income);
		career_money.AddValue(income);
	}

  public void BombDropped()
  {
    AchievementTracker.Instance().IncrementAchievementStat("total_bombs_dropped");
  }

	public void CoinHit()
	{
		AddMoney(GetScoringObjectByName("coin_upgrader").GetPointValue());
    AchievementTracker.Instance().IncrementAchievementStat("total_coins_hit");
	}

	public void PegHit()
	{
    AddMoney(peg_upgrader.GetPointValue());
    AchievementTracker.Instance().IncrementAchievementStat("total_pegs_hit");
	}

	public void BumperHit()
	{
    AddMoney(bumper_upgrader.GetPointValue());
    AchievementTracker.Instance().IncrementAchievementStat("total_bumpers_hit");
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

  private ScoringObject GetScoringObjectByName(string name)
  {
    if (scoring_objects.ContainsKey(name))
      return scoring_objects[name];
    else
    {
      Debug.LogException(new System.Exception(name + " tried to access a scoring object by name and failed."));
      return null;
    }
  }
}