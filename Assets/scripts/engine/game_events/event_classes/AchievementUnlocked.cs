using UnityEngine;
using System.Collections;

public class AchievementUnlocked : GameEvent 
{
  public AchievementPopupInfo popup_info;

  public AchievementUnlocked(AchievementPopupInfo popup_info)
    : base("achievement_unlocked_event")
  {
    this.popup_info = popup_info;
  }
}
