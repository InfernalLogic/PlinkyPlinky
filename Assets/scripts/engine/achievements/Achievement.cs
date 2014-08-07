using UnityEngine;
using System.Collections;

public class Achievement : MonoBehaviour
{
  [SerializeField]
  private int unlock_at = 0;
  public GUIContent thumbnail;
  [SerializeField]
  private AchievementPopupInfo popup_info;

  private SavedBool is_unlocked;
  private SavedStat tracked_stat;

  void Awake()
  {
    tracked_stat = transform.parent.GetComponent<SavedStat>();
    is_unlocked = GetComponent<SavedBool>();
    popup_info.achievement_name = name;
    popup_info.achievement_text_number = AbbreviatedUnlockAt();
    thumbnail.tooltip = "Unlocked at " + AbbreviatedUnlockAt();
  }
  
  public void CheckForCompletedAchievement()
  {
    if (tracked_stat.GetValue() >= unlock_at && !IsUnlocked())
    {
      is_unlocked.Set(true);
      AchievementUnlockedEvents.Publish(new AchievementUnlocked(popup_info));
    }
  }

  public bool IsUnlocked()
  {
    return is_unlocked.IsTrue();
  }

  private string AbbreviatedUnlockAt()
  {
    if (unlock_at >= 1000)
      return (unlock_at / 1000).ToString() + "K";
    else
      return unlock_at.ToString();
  }
}
