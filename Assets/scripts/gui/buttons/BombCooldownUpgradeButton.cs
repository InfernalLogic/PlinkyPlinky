using UnityEngine;
using System.Collections;

public class BombCooldownUpgradeButton : UpgradeButton 
{
  private BombCooldownUpgrader bomb_cooldown_upgrader;

  void Awake()
  {
    bomb_cooldown_upgrader = FindObjectOfType<BombCooldownUpgrader>();
  }

  protected override void DisplayTextLabel()
  {
    GUI.Label(label_display_rect.GetRect(),
              button_text + "\nCost: " + target_upgrade.GetUpgradeCost() + "\nCooldown: " + 
              (BombScript.GetBaseCooldown() - ((float)bomb_cooldown_upgrader.GetValue() * 0.1f)) + "s",
              label_style);
  }
}
