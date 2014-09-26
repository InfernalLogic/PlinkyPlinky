using UnityEngine;
using System.Collections;

public class PlinkagonUpgradeButton : UpgradeButton 
{
  private PlinkagonUpgrade plinkagon_upgrade;

  void Awake()
  {
    plinkagon_upgrade = target_upgrade as PlinkagonUpgrade;
  }

  protected override void DisplayTextLabel()
  {
    GUI.Label(label_display_rect.GetRect(),
              button_text +
              "\nCurrent: " + plinkagon_upgrade.GetValue() +
              "\nChance: " + plinkagon_upgrade.GetChanceToProc() + "%",
              label_style);
  }

}
