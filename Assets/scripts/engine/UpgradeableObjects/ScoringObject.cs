using UnityEngine;
using System.Collections;

abstract public class ScoringObject : UpgradeableObject 
{
	abstract public int GetPointValue();
  abstract protected void RecalculatePointValue();

  protected int point_value;

  new void Awake()
  {
    LoadEngine();
    Load();
    RecalculateUpgradeCost();
    RecalculatePointValue();
  }

	public virtual void Reset()
	{
    base.Reset();
		RecalculateUpgradeCost();
	}

	public override void Upgrade()
	{
		if (PlayerHasEnoughMoney())
		{
      ++value;
			engine.player_stats.SpendMoney(upgrade_cost);
      RecalculatePointValue();
			RecalculateUpgradeCost();
		}
		else
		{
			Debug.Log("Not enough moneys!");
		}
	}
}
