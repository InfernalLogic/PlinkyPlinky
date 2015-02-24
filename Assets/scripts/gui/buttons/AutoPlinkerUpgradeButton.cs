using UnityEngine;
using System.Collections;

public class AutoPlinkerUpgradeButton : UpgradeButton
{
  private AutoPlinker auto_plinker;

  protected override void Awake()
  {
    auto_plinker = FindObjectOfType<AutoPlinker>();
    base.Awake();
  }

  protected override void DisplayTextLabel()
  {
    GUI.Label(label_display_rect.GetRect(),
              button_text + "\nCost: " + target_upgrade.GetUpgradeCost() + "\nCooldown: " +
              (auto_plinker.Cooldown.ToString("n2")) + "s",
              label_style);
  }
}
