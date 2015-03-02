using UnityEngine;
using System.Collections;

public class PlinkagonPointBuyer : UpgradeableObject 
{
  public override void Upgrade()
  {
    if (PlayerHasEnoughCurrency())
    {
      MoneyTracker.Instance.SpendMoney(upgrade_cost);
      ++value;
      GameEvents.Publish(new MoneyEarnedGameEvent(1, "plinkagon_point_earned_event"));
      RecalculateUpgradeCost();
    }
    else
    {
      Debug.Log(this.name + ": Not enough money!");
      Debug.Log("costs: " + this.upgrade_cost);
    }
  }

  public override void RecalculateUpgradeCost()
  {
    upgrade_cost = 10000 + (int)((Mathf.Pow((float)(value + 1), 1.3f) * 500));
  }
}
