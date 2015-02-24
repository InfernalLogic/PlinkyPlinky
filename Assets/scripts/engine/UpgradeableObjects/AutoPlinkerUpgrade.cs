using UnityEngine;
using System.Collections;

public class AutoPlinkerUpgrade : UpgradeableObject 
{
  AutoPlinker auto_plinker;

  private void Start()
  {
    base.Start();
    auto_plinker = FindObjectOfType<AutoPlinker>();
  }

  public override void RecalculateUpgradeCost()
  {
    upgrade_cost = (int)((Mathf.Pow((float)(value), 1.5f) * 5000)) + 10000;
  }

  public override void Upgrade()
  {
    if (PlayerHasEnoughCurrency() && UpgradesNotMaxedOut())
    {
      MoneyTracker.Instance().SpendMoney(upgrade_cost);
      ++value;
      RecalculateUpgradeCost();
      auto_plinker.StartInitialCooldown();
    }
  }
}
