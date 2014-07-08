using UnityEngine;
using System.Collections;

public class UnlockablesMenu : HUDField
{
  [SerializeField]
  private GUIStyle button_style;

  protected override void DisplayGUIElements()
  {
    if (GUI.Button(new Rect(10, 10, 300, 100), "Unlock level 3", button_style))
    {
      engine.level_handler.levels[3].GetComponent<LevelUpgrader>().Upgrade();
      engine.level_handler.LoadLevel(3);
    }
  }
}
