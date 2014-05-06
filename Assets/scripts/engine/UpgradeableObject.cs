using UnityEngine;
using System.Collections;

abstract public class UpgradeableObject : PlinkyObject 
{
	protected int upgrades;
	protected int upgrade_cost;
	protected int max_upgrades = 0;

	abstract public void Upgrade();
	abstract public void CalculateUpgradeCost();
	abstract public void Reset();
	abstract protected void Initialize(); 

	void Start()
	{
		upgrades = PlayerPrefs.GetInt(gameObject.name +"upgrades", 0);
		CalculateUpgradeCost();
		Initialize ();
	}

	void OnDestroy()
	{
		PlayerPrefs.SetInt(gameObject.name +"upgrades", upgrades);
	}

	public int GetUpgrades()
	{
		return upgrades;
	}

	public int GetUpgradeCost()
	{
		return upgrade_cost;
	}
}
