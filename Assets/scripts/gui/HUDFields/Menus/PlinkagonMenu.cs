using UnityEngine;
using System.Collections;

public class PlinkagonMenu : HUDField
{
  [SerializeField]
  private GUIStyle button_style;
  [SerializeField]
  private GUIStyle button_disabled_mask;
  [SerializeField]
  private ScrollViewInfo scroll_view;
  [SerializeField]
  private StatDisplayer plinkagon_points;

  private Button[] upgrade_buttons;

  void Start()
  {
    upgrade_buttons = GetComponentsInChildren<Button>();
  }

  protected override void DisplayGUIElements()
  {
    scroll_view.Begin();

      foreach (Button button in upgrade_buttons)
      {
        button.Display();
      }

    scroll_view.End();
  }
}