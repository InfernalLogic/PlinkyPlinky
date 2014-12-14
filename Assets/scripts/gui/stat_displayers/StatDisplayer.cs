using UnityEngine;
using System.Collections;

public class StatDisplayer : MonoBehaviour 
{
  [SerializeField]
  private ScalingRect display_rect;
  [SerializeField]
  private AchievementChain tracked_stat;
  [SerializeField]
  private string label_text;
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

  public void Display(GUIStyle label_display_style)
  {
    original_alignment = label_display_style.alignment;
    GUI.DrawTexture(display_rect.GetRect(), stat_display_background);
    GUI.Label(display_rect.GetRect(), label_text, label_display_style);

    label_display_style.alignment = TextAnchor.UpperRight;
    GUI.Label(display_rect.GetRect(), tracked_stat.GetTrackedStatValue().ToString(), label_display_style);
    label_display_style.alignment = original_alignment;
  }

  public float GetHeight()
  {
    return display_rect.GetDimensions().height;
  }

  public float GetY()
  {
    return display_rect.GetDimensions().y;
  }
}
