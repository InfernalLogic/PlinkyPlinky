using UnityEngine;
using System.Collections;

public class Achievement : MonoBehaviour
{
  [SerializeField]
  private int unlock_at = 0;
  [SerializeField]
  private int plinkagon_point_value = 1;
  [SerializeField]
  public GUIContent thumbnail;
  [SerializeField]
  private AchievementPopupInfo popup_info;

  private SavedStat is_unlocked;
  private SavedStat tracked_stat;

  void Awake()
  {
    tracked_stat = transform.parent.GetComponent<SavedStat>();
    is_unlocked = GetComponent<SavedStat>();
    popup_info.achievement_name = name;
    popup_info.achievement_text_number = AbbreviatedUnlockAt();
  }
  
  public void CheckForCompletedAchievement()
  {
    if (tracked_stat.GetValue() >= unlock_at && !IsUnlocked())
    {
      is_unlocked.AddValue(1);
      AchievementUnlockedEvents.Publish(new AchievementUnlocked(popup_info));
    }
  }

  public bool IsUnlocked()
  {
    return is_unlocked.GetValue() >= 1;
  }

  private string AbbreviatedUnlockAt()
  {
    if (unlock_at >= 1000)
      return (unlock_at / 1000).ToString() + "K";
    else
      return unlock_at.ToString();
  }
}
