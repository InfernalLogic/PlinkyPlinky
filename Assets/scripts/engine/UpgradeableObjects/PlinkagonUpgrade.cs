using UnityEngine;
using System.Collections;

public class PlinkagonUpgrade : UpgradeableObject
{
  [SerializeField]
  private float percent_chance_per_level = 2.0f;

  void Awake()
  {
    is_plinkagon_upgrade = true;
  }

  public override void Upgrade()
  {
    if (PlayerHasEnoughCurrency() && UpgradesNotMaxedOut())
    {
      MoneyTracker.Instance().SpendPlinkagonPoints(upgrade_cost);
      ++value;
      UpgradeEvents.Publish(UpgradeEvents.clone_balls_upgraded);
    }
  }

  public override void RecalculateUpgradeCost()
  {
    upgrade_cost = 1;
  }

  public float GetChanceToProc()
  {
    return percent_chance_per_level * (float)value;
  }
}
