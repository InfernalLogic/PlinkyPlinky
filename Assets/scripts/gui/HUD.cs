using UnityEngine;
using System.Collections;

public class HUD : PlinkyObject
{
  public HUDField[] HUD_fields;

  private float last_screen_width;

  void Awake()
  {
    base.Awake();
    last_screen_width = Screen.width;
  }

  void OnGUI()
  {
    foreach (HUDField field in HUD_fields)
    {
      field.Display();
    }

    if (ScreenDimensionsHaveBeenChanged())
    {
      ResetLastScreenWidth();

      ScalingRect[] scaling_rects = GetAllScalingRects();

      foreach (ScalingRect rect in scaling_rects)
      {
        rect.RecalculateDimensions();
      }
    }
  }

  private bool ScreenDimensionsHaveBeenChanged()
  {
    return last_screen_width != Screen.width;
  }

  private void ResetLastScreenWidth()
  {
    last_screen_width = Screen.width;
  }

  private ScalingRect[] GetAllScalingRects()
  {
    return Object.FindObjectsOfType<ScalingRect>();
  }
}