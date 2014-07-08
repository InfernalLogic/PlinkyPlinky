using UnityEngine;
using System.Collections;

public class LevelUpgrader : UpgradeableObject 
{
	public bool is_unlocked_on_reset = false;

	protected override void Initialize()	
	{
		upgrade_id = gameObject.name;
		//Debug.Log ("level upgrade_id: " + upgrade_id);
		SetMaxUpgrades();
	} 

	public override void Upgrade()
	{

		if (HasEnoughMoney() && upgrades < max_upgrades)
		{
			upgrades = 1;
			engine.player_stats.SpendMoney(upgrade_cost);
			engine.level_handler.LoadUnlockedLevels();
			Debug.Log(gameObject.name + " Upgrade() called");
		}
		else
		{
			if (!HasEnoughMoney())
			{
				Debug.Log("Not enough moneys! " + gameObject.name);
			}

			if (upgrades >= max_upgrades)
			{
				Debug.Log ("Max upgrades reached!" + gameObject.name);
				Debug.Log ("upgrades: " + upgrades + " max_upgrades: " + max_upgrades);
				Debug.Log (IsUnlocked());
			}
		}

	}

	public override void CalculateUpgradeCost()	
	{
		upgrade_cost = 0;
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

	public void SetMaxUpgrades()
	{
		max_upgrades = 1;
	}

	public void Save()
	{
		upgrade_id = gameObject.name;
		PlayerPrefs.SetInt(upgrade_id +"_upgrades", upgrades);
		Debug.Log (upgrade_id +" saved as: " + PlayerPrefs.GetInt(upgrade_id +"_upgrades", 0));
	}
}
