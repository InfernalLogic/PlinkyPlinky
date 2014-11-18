using UnityEngine;
using System.Collections;

public class UpgradeMenu : HUDField 
{
  [SerializeField]
  private GUIStyle button_style;
  [SerializeField]
  private GUIStyle button_disabled_mask;
  [SerializeField]
  private ScrollViewInfo scroll_view;

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