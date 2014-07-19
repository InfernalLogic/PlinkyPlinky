using UnityEngine;
using System.Collections;

public class Achievement : MonoBehaviour
{
  [SerializeField]
  private int unlock_at = 0;
  [SerializeField]
  private string unlock_message;

  private SavedStat is_unlocked;
  private SavedStat tracked_stat;


  void Awake()
  {
    tracked_stat = transform.parent.GetComponent<SavedStat>();
    is_unlocked = GetComponent<SavedStat>();
    Debug.Log(tracked_stat.name + " tracked by " + name);
  }
  
  public void CheckForCompletedAchievement()
  {
    if (tracked_stat.GetValue() >= unlock_at && !IsUnlocked())
    {
      is_unlocked.AddValue(1);
      GetComponentInParent<AchievementTracker>().EnqueueAchievementPopup(unlock_message);
      Debug.Log(name + " unlocked");
    }
  }

  public bool IsUnlocked()
  {
    return is_unlocked.GetValue() >= 1;
  }
}
