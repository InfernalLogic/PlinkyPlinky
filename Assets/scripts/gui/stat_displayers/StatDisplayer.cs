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
  [SerializeField]
  private int row_number = 0;

  private TextAnchor original_alignment;

  void Awake()
  {
    InitializeDisplayRect();
  }

  private void InitializeDisplayRect()
  {
    display_rect.SetX(0.0f);
    display_rect.SetY((float)row_number * 12.0f + 2.0f);
    display_rect.SetWidth(29.0f);
    display_rect.SetHeight(4.0f);
  }

  public void Display(GUIStyle lable_display_style)
  {
    GUI.DrawTexture(display_rect.GetRect(), stat_display_background);
    GUI.Label(display_rect.GetRect(), lable_text, lable_display_style);

    lable_display_style.alignment = TextAnchor.UpperRight;
    GUI.Label(display_rect.GetRect(), tracked_stat.GetTrackedStatValue().ToString(), lable_display_style);
    lable_display_style.alignment = original_alignment;
  }

  public float GetHeight()
  {
    return display_rect.GetDimensions().height;
  }
}
