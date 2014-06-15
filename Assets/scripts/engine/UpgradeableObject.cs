using UnityEngine;
using System.Collections;

abstract public class UpgradeableObject : PlinkyObject 
{
	protected int upgrades;
	protected int upgrade_cost;
	protected int max_upgrades = 0;
	protected string upgrade_id;

	abstract public void Upgrade();
	abstract public void CalculateUpgradeCost();
	abstract public void Reset();
	abstract protected void Initialize(); 

	void Start()
	{
		Initialize();
		Load ();
		CalculateUpgradeCost();

		if (engine == null)
		{
			Debug.Log ("engine not loaded properly in " + gameObject.name);
		}
	}

	void OnDestroy()
	{
		Save ();
	}

	public int GetUpgrades()
	{
		return upgrades;
	}

	public int GetUpgradeCost()
	{
		return upgrade_cost;
	}

	protected bool HasEnoughMoney()
	{
		if (engine.player_stats.GetCurrentMoney() >= upgrade_cost)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public void Save()
	{
		if (!IsClone())
		{
			PlayerPrefs.SetInt(upgrade_id +"_upgrades", upgrades);
			Debug.Log (upgrade_id +" saved as: " + PlayerPrefs.GetInt(upgrade_id +"_upgrades", 0));
		}
	}

	public void Load()
	{
		if (!IsClone ())
		{
			upgrades = PlayerPrefs.GetInt(upgrade_id +"_upgrades", 0);
			Debug.Log (upgrade_id + " initialized to: " + upgrades);
		}
	}

	bool IsClone()
	{
		return upgrade_id.Contains("(Clone)");
	}
}
