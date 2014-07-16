using UnityEngine;
using System.Collections;

abstract public class ScoringObject : UpgradeableObject 
{
	abstract public int GetPointValue();
	
	public void Scored()
	{
		engine.player_stats.AddMoney(GetPointValue());
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
			GetPointValue();
			RecalculateUpgradeCost();
		}
		else
		{
			Debug.Log("Not enough moneys!");
		}
	}
}
