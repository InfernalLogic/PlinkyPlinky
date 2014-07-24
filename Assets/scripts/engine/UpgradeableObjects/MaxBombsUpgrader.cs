using UnityEngine;
using System.Collections;

public class MaxBombsUpgrader : UpgradeableObject 
{
  public override void RecalculateUpgradeCost()
  {
    int starting_index_adjuster = default_value_on_reset - 1;

    upgrade_cost = (int)((Mathf.Pow((float)(value - starting_index_adjuster), 1.4f) * 1000));
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
