using UnityEngine;
using System.Collections;

abstract public class ScoringObject : UpgradeableObject 
{
	abstract public int CalculatePointValue();
	
	public void Scored()
	{
		engine.player_stats.AddMoney(CalculatePointValue());
	}

	public override void Reset()
	{
		upgrades = 0;
		CalculatePointValue();
		CalculateUpgradeCost();
	}

	public override void Upgrade()
	{
		if (HasEnoughMoney())
		{
			++upgrades;
			engine.player_stats.SpendMoney(upgrade_cost);
			CalculatePointValue();
			CalculateUpgradeCost();
		}
		else
		{
			Debug.Log("Not enough moneys!");
		}
	}
}
