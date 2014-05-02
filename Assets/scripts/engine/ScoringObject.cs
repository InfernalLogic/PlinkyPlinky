using UnityEngine;
using System.Collections;

abstract public class ScoringObject : UpgradeableObject 
{
	protected int point_value= 0;
	abstract public void CalculatePointValue();

	protected override void Initialize()
	{
		CalculatePointValue();
	}

	public int GetPointValue()
	{
		return point_value;
	}

	public void Scored()
	{
		engine.player_stats.AddMoney(point_value);
	}

	public override void Reset()
	{
		upgrades = 0;
		CalculatePointValue();
		CalculateUpgradeCost();
	}

	public override void Upgrade()
	{
		if (engine.player_stats.GetCurrentMoney() >= upgrade_cost)
		{
			++upgrades;
			engine.player_stats.SpendMoney(upgrade_cost);
			CalculatePointValue();
			CalculateUpgradeCost();
			engine.hud.UpdateMoney();
		}
		else
		{
			Debug.Log("Not enough moneys!");
		}
	}
}
