using UnityEngine;
using System.Collections;

public class AchievementChain : MonoBehaviour 
{
  [SerializeField]
  private string relevant_event_name;

  private Achievement[] achievements;
  private SavedStat tracked_stat;

  void Awake()
  {
    tracked_stat = GetComponent<SavedStat>();

    if (!tracked_stat)
    {
      Debug.LogError("tracked stat not loaded for" + name);
    }

    achievements = GetComponentsInChildren<Achievement>();
  }

  public void Increment()
  {
    Add(1);
  }

  public void Add(int value)
  {
    tracked_stat.AddValue(value);
    foreach (Achievement achieve in achievements)
    {
      achieve.CheckForCompletedAchievement();
    }
  }

  public string GetRelevantEventName()
  {
    return relevant_event_name;
  }
}
