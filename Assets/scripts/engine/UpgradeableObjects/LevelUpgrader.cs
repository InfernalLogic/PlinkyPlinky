using UnityEngine;
using System.Collections;

public class LevelUpgrader : UpgradeableObject 
{
	public bool is_unlocked_on_reset = false;

	protected override void Initialize()	
	{
		upgrade_id = gameObject.name;
		//Debug.Log ("level upgrade_id: " + upgrade_id);
		max_upgrades = 1;
	} 

	public override void Upgrade()
	{
		/*
		if (HasEnoughMoney() && upgrades < max_upgrades)
		{
			upgrades = 1;
			engine.player_stats.SpendMoney(upgrade_cost);
			engine.hud.UpdateMoney();
		}
		else
		{
			Debug.Log("Not enough moneys!");
		}
		*/
	}

	public override void CalculateUpgradeCost()	
	{

	}

	public override void Reset()	
	{
		if (is_unlocked_on_reset == true)
		{
			upgrades = 1;
		}
		else
		{
			upgrades = 0;
		}

	}

	public bool IsUnlocked()
	{
		if (upgrades != 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
