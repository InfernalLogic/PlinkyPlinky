using UnityEngine;
using System.Collections;

public class AchievementChainDisplayer : MonoBehaviour 
{
  [SerializeField]
  private ScalingRect first_achievement_position;
  [SerializeField]
  private AchievementChain target_chain;
  [SerializeField]
  private Texture locked_achievement_mask;
  [SerializeField]
  private float buffer = 0.0f;

  private Achievement[] achievements;

  private Rect current_display_rect;

  void Awake()
  {
    achievements = target_chain.GetComponentsInChildren<Achievement>();
    current_display_rect = first_achievement_position.GetRect();
  }

  public void Display(GUIStyle style)
  {
    for (int i = 0; i < achievements.Length; ++i)
    {
      GUI.Label(CurrentAchievementDisplayRect(i), achievements[i].thumbnail, style);

      if (!achievements[i].IsUnlocked())
        GUI.DrawTexture(CurrentAchievementDisplayRect(i), locked_achievement_mask);
    }
  }

  private Rect CurrentAchievementDisplayRect(int index)
  {
    current_display_rect.x = first_achievement_position.GetRect().x + ((first_achievement_position.GetRect().width + buffer)* index);
    return current_display_rect;
  }
}
