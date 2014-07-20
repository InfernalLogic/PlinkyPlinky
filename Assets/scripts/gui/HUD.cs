using UnityEngine;
using System.Collections;

public class HUD : Singleton<HUD>
{
  public HUDField[] HUD_fields;

  private float last_screen_width;

  void Awake()
  {
    last_screen_width = Screen.width;
    Screen.SetResolution(960, 600, false, 60);
  }

  void OnGUI()
  {
    if (ScreenDimensionsHaveBeenChanged())
    {
      ResetLastScreenWidth();
      ResizeAllScalingRects();
    }

    foreach (HUDField field in HUD_fields)
    {
      field.Display();
    }
  }

  private void ResizeAllScalingRects()
  {
    ScalingRect[] scaling_rects = GetAllScalingRects();

    foreach (ScalingRect rect in scaling_rects)
    {
      rect.RecalculateDimensions();
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