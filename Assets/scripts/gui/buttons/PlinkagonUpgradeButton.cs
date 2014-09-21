using UnityEngine;
using System.Collections;

public class PlinkagonUpgradeButton : UpgradeButton 
{
  [SerializeField]
  private PlinkagonUpgrade clone_ball_upgrader;

  protected override void DisplayTextLabel()
  {
    GUI.Label(label_display_rect.GetRect(),
              button_text + 
              "\nCurrent: " + clone_ball_upgrader.GetValue() +
              "\nChance: " + clone_ball_upgrader.GetChanceToProc() + "%",
              label_style);
  }

}
