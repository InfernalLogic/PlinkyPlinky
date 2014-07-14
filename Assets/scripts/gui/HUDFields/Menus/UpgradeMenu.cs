using UnityEngine;
using System.Collections;

public class UpgradeMenu : HUDField 
{
  [SerializeField]
  private GUIStyle button_style;

  [SerializeField]
  private GUIStyle button_disabled_mask;

  [SerializeField]
  private Button[] upgrade_buttons;

  protected override void DisplayGUIElements()
  {
    foreach (Button button in upgrade_buttons)
    {
      button.Display();
    }
  }
}