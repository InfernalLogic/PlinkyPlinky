using UnityEngine;
using System.Collections;

public class MaxBombsUpgrader : UpgradeableObject 
{
  public override void RecalculateUpgradeCost()
  {
    int starting_index_adjuster = default_value_on_reset - 1;

    upgrade_cost = (ulong)((Mathf.Pow((float)(value - starting_index_adjuster), 1.4f) * 1000));
  }

  public override void Upgrade()
  {
    if (PlayerHasEnoughCurrency() && UpgradesNotMaxedOut())
    {
      MoneyTracker.Instance.SpendMoney(upgrade_cost);
      ++value;
      RecalculateUpgradeCost();
      UpgradeEvents.Publish(UpgradeEvents.max_bombs_upgraded);
    }
  }
}
