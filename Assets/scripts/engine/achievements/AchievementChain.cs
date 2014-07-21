using UnityEngine;
using System.Collections;

public class AchievementChain : MonoBehaviour 
{
  [SerializeField]
  private GameEvent relevant_event;

  private Achievement[] achievements;
  private SavedStat tracked_stat;

  void Awake()
  {
    tracked_stat = GetComponent<SavedStat>();

    if (!tracked_stat)
    {
      Debug.Log("tracked stat not loaded for" + name);
    }

    achievements = GetComponentsInChildren<Achievement>();

    Debug.Log(achievements.Length + " achievements found in " + name);
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

  public GameEvent GetRelevantEvent()
  {
    return relevant_event;
  }
}
