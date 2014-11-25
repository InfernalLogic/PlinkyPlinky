using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpgradeHandler : Singleton<UpgradeHandler> 
{
  [SerializeField]
  private bool hard_reset_on_load_enabled = false;

  private IDictionary<string, ScoringObjectUpgrade> scoring_objects = new Dictionary<string, ScoringObjectUpgrade>();

  new void Awake()
  {
    base.Awake();

    InitializeScoringObjectsDictionary();

    if (hard_reset_on_load_enabled)
    {
      PlayerPrefs.DeleteAll();
      HardResetStats();
    }
  }

  private void InitializeScoringObjectsDictionary()
  {
    ScoringObjectUpgrade[] scoring_objects_in_children = GetComponentsInChildren<ScoringObjectUpgrade>();

    foreach (ScoringObjectUpgrade scoring_object in scoring_objects_in_children)
    {
      scoring_objects.Add(scoring_object.GetRelevantEventName(), scoring_object);
    }
  }

	public void ResetStats()
	{	
		SavedStat[] stats = Resources.FindObjectsOfTypeAll<SavedStat>();

    foreach (SavedStat element in stats)
		{
			element.Reset();
		}

    RebuildStats();
	}

  public void HardResetStats()
  {
    SavedStat[] stats = Resources.FindObjectsOfTypeAll<SavedStat>();

    foreach (SavedStat element in stats)
    {
      element.HardReset();
    }

    RebuildStats();
    GameEvents.Publish(GameEvents.game_reset_event);
  }

  private static void RebuildStats()
  {
    RecalculateUpgradeCosts();
    RecalculateScoringObjectPointValues();

    LevelHandler.Instance().UpdateUnlockedLevels();
  }

  private static void RecalculateScoringObjectPointValues()
  {
    ScoringObjectUpgrade[] scoring_objects = Resources.FindObjectsOfTypeAll<ScoringObjectUpgrade>();

    foreach (ScoringObjectUpgrade scoring_object in scoring_objects)
    {
      scoring_object.RecalculatePointValue();
    }
  }

  private static void RecalculateUpgradeCosts()
  {
    UpgradeableObject[] upgrades = Resources.FindObjectsOfTypeAll<UpgradeableObject>();

    foreach (UpgradeableObject upgrade in upgrades)
    {
      upgrade.RecalculateUpgradeCost();
    }
  }

  public ScoringObjectUpgrade FindScoringObjectByKey(string key)
  {
    if (scoring_objects.ContainsKey(key))
      return scoring_objects[key];
    else
    {
      Debug.LogError(key + " not found in scoring_objects!");
      return null;
    }
  }

  public bool ContainsKey(string key)
  {
    return scoring_objects.ContainsKey(key);
  }
}