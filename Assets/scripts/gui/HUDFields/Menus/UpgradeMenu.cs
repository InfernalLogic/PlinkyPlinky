using UnityEngine;
using System.Collections;

public class UpgradeMenu : HUDField 
{
  [SerializeField]
  private GUIStyle button_style;

  [SerializeField]
  private GUIStyle button_disabled_mask;

  private Button[] upgrade_buttons;

  void Start()
  {
    upgrade_buttons = GetComponentsInChildren<Button>();
  }

  protected override void DisplayGUIElements()
  {
    foreach (Button button in upgrade_buttons)
    {
      button.Display();
    }
  }
}