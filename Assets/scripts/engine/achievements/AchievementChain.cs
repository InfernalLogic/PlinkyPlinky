using UnityEngine;
using System.Collections;

public class AchievementChain : MonoBehaviour 
{
  [SerializeField]
  private GameObject relevant_event_source;

  private Achievement[] achievements;
  private SavedStat tracked_stat;
  private GameEvent relevant_event;

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
}
