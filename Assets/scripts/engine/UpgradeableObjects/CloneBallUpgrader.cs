using UnityEngine;
using System.Collections;

public class CloneBallUpgrader : UpgradeableObject 
{
  [SerializeField]
  private float percent_chance_per_level = 2.0f;

  public override void Upgrade()
  {
    if (PlayerHasEnoughMoney() && UpgradesNotMaxedOut())
    {
      MoneyTracker.Instance().SpendMoney(upgrade_cost);
      ++value;
      RecalculateUpgradeCost();
      UpgradeEvents.Publish(UpgradeEvents.clone_balls_upgraded);
    }
  }

  public override void RecalculateUpgradeCost()
  {
    upgrade_cost = 1;
  }

  public float GetChanceToSpawn()
  {
    return percent_chance_per_level * (float)value;
  }
}
