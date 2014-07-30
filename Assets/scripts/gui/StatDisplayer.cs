using UnityEngine;
using System.Collections;

public class StatDisplayer : MonoBehaviour 
{
  [SerializeField]
  private ScalingRect display_rect;
  [SerializeField]
  private AchievementChain tracked_stat;
  [SerializeField]
  private string lable_text;
  [SerializeField]
  private Texture stat_display_background;

  public void Display(GUIStyle lable_display_style)
  {
    TextAnchor original_alignment = lable_display_style.alignment;

    GUI.DrawTexture(display_rect.GetRect(), stat_display_background);
    GUI.Label(display_rect.GetRect(), lable_text, lable_display_style);
    lable_display_style.alignment = TextAnchor.UpperRight;
    GUI.Label(display_rect.GetRect(), tracked_stat.GetTrackedStatValue().ToString(), lable_display_style);
    lable_display_style.alignment = original_alignment;
  }

}
