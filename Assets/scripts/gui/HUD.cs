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
      Debug.Log("screen dimensions changed");
      ResetLastScreenWidth();

      ScalingRect[] scaling_rects = GetAllScalingRects();

      Debug.Log("Found " + scaling_rects.Length + " scaling rects.");

      foreach (ScalingRect rect in scaling_rects)
      {
        rect.CalculateDimensions();
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