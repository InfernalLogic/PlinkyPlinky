using UnityEngine;
using System.Collections;

public class ScrollViewInfo : MonoBehaviour
{
  [SerializeField]
  private ScalingRect view_rect;
  [SerializeField]
  private ScalingRect scroll_view_position_rect;

  private Vector2 scroll_view_position = Vector2.zero;

  public void Begin()
  {
    scroll_view_position = GUI.BeginScrollView(scroll_view_position_rect.GetRect(), scroll_view_position, view_rect.GetRect());
  }

  public void End()
  {
    GUI.EndScrollView();
  }

  public void SetViewHeight(float view_height)
  {
    view_rect.SetHeight(view_height);
  }
}
