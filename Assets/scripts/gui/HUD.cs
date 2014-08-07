using UnityEngine;
using System.Collections;

public class HUD : Singleton<HUD>
{
  [SerializeField]
  private HUDField[] HUD_fields;
  [SerializeField]
  private GUIStyle tooltip_style;
  [SerializeField]
  private Texture tooltip_background;

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

    DisplayToolTips();
  }

  private void DisplayToolTips()
  {
    if (GUI.tooltip != "")
    {
      GUI.DrawTexture(new Rect (Input.mousePosition.x, (31 * Screen.height) / 32 - Input.mousePosition.y, 10 * GUI.tooltip.Length + 10, 30), tooltip_background);
      GUI.Label(new Rect(Input.mousePosition.x, (31 * Screen.height) / 32 - Input.mousePosition.y, 10 * GUI.tooltip.Length, 20), GUI.tooltip, tooltip_style);
    }
  }

  private void ResizeAllScalingRects()
  {
    ScalingRect[] scaling_rects = GetAllScalingRects();

    foreach (ScalingRect rect in scaling_rects)
    {
      rect.RecalculateRect();
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