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
    string cooldown;

    if (auto_plinker.Cooldown > 10.0f)
      cooldown = "N/A";
    else
    {
      cooldown = auto_plinker.Cooldown.ToString("n2");
      cooldown += "s";
    }

    GUI.Label(label_display_rect.GetRect(),
              button_text + "\nCost: " + target_upgrade.GetUpgradeCost() + "\nCooldown: " +
              cooldown, label_style);
  }
}
