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
      Events.PublishReset(ResetType.HARD);
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