using UnityEngine;
using System.Collections;

public class BombCooldownUpgradeButton : UpgradeButton 
{
  [SerializeField]
  private BombCooldownTimer bomb_cooldown_timer;

  void Awake()
  {
    bomb_cooldown_timer = FindObjectOfType<BombCooldownTimer>();
  }

  protected override void DisplayTextLabel()
  {
    GUI.Label(label_display_rect.GetRect(),
              button_text + "\nCost: " + target_upgrade.GetUpgradeCost() + "\nCooldown: " + 
              (bomb_cooldown_timer.GetDuration()) + "s",
              label_style);
  }
}
