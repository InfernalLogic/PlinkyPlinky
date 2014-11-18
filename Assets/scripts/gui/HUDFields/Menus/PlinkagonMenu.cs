using UnityEngine;
using System.Collections;

public class PlinkagonMenu : HUDField
{
  [SerializeField]
  private GUIStyle button_style;
  [SerializeField]
  private GUIStyle button_disabled_mask;
  [SerializeField]
  private GUIStyle label_style;
  [SerializeField]
  private ScrollViewInfo scroll_view;
  [SerializeField]
  private ScalingRect plinkagon_point_display_rect;

  private Button[] upgrade_buttons;
  private TextAnchor original_alignment;

  void Start()
  {
    upgrade_buttons = GetComponentsInChildren<Button>();
  }

  protected override void DisplayGUIElements()
  {
    scroll_view.Begin();

      DisplayCurrentPlinkagonPoints();

      foreach (Button button in upgrade_buttons)
      {
        button.Display();
      }

    scroll_view.End();
  }

  void DisplayCurrentPlinkagonPoints()
  {
    GUI.Label(plinkagon_point_display_rect.GetRect(), "Plinkagon Points: ", label_style);

    label_style.alignment = TextAnchor.MiddleRight;
    GUI.Label(plinkagon_point_display_rect.GetRect(), MoneyTracker.Instance().GetCurrentPlinkagonPoints().ToString(), label_style);
    label_style.alignment = original_alignment;
  }
}