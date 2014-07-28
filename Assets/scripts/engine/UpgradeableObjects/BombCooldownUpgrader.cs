using UnityEngine;
using System.Collections;

public class BombCooldownUpgrader : UpgradeableObject
{
  public override void RecalculateUpgradeCost()
  {
    upgrade_cost = (int)((Mathf.Pow((float)(value + 1), 1.3f) * 750));
  }

  public override void Upgrade()
  {
    if (PlayerHasEnoughMoney() && UpgradesNotMaxedOut())
    {
      PlayerStats.Instance().SpendMoney(upgrade_cost);
      ++value;
      RecalculateUpgradeCost();
    }
  }
}
