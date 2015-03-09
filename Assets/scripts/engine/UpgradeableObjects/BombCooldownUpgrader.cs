using UnityEngine;
using System.Collections;

public class BombCooldownUpgrader : UpgradeableObject
{
  [SerializeField]
  private float cooldown_reduction_per_level = 0.1f;

  public override void RecalculateUpgradeCost()
  {
    upgrade_cost = (ulong)((Mathf.Pow((float)(value + 1), 1.3f) * 750));
  }

  public override void Upgrade()
  {
    if (PlayerHasEnoughCurrency() && UpgradesNotMaxedOut())
    {
      MoneyTracker.Instance.SpendMoney(upgrade_cost);
      ++value;
      RecalculateUpgradeCost();
      UpgradeEvents.Publish(UpgradeEvents.bomb_cooldown_upgraded);
    }
  }

  public float GetCooldownReduction()
  {
    return (value * cooldown_reduction_per_level);
  }
}
