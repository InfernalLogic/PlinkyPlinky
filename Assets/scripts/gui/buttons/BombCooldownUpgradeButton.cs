using UnityEngine;
using System.Collections;

public class BombCooldownUpgradeButton : UpgradeButton 
{
  [SerializeField]
  private BombCooldownTimer bomb_cooldown_timer;

  protected override void Awake()
  {
    bomb_cooldown_timer = FindObjectOfType<BombCooldownTimer>();
    base.Awake();
  }

  protected override void DisplayTextLabel()
  {
    GUI.Label(label_display_rect.GetRect(),
              button_text + "\nCost: " + target_upgrade.GetUpgradeCost() + "\nCooldown: " + 
              (bomb_cooldown_timer.GetDuration()) + "s",
              label_style);
  }

  protected override void DisplayMaxUpgradesReached()
  {
    GUI.Label(display_rect.GetRect(), "", button_style);
    GUI.Label(label_display_rect.GetRect(), max_upgrades_reached_message + "\nCooldown: " +
              (bomb_cooldown_timer.GetDuration()) + "s", label_style);
  }
}
