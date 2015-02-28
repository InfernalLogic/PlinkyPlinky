using UnityEngine;
using System.Collections;

public class MaxBombsUpgradeButton : UpgradeButton 
{
  private MaxBombsUpgrader max_bombs_upgrader;

  protected override void Awake()
  {
    max_bombs_upgrader = FindObjectOfType<MaxBombsUpgrader>();
    base.Awake();
  }

  protected override void DisplayTextLabel()
  {
    GUI.Label(label_display_rect.GetRect(),
              button_text + "\nCost: " + target_upgrade.GetUpgradeCost() + "\nMax: " + 
              max_bombs_upgrader.GetValue(), 
              label_style);
  }

  protected override void DisplayMaxUpgradesReached()
  {
    GUI.Label(display_rect.GetRect(), "", button_style);
    GUI.Label(label_display_rect.GetRect(), max_upgrades_reached_message + "\nMax: " + 
              max_bombs_upgrader.GetValue(), label_style);
  }
}
