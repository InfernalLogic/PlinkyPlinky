using UnityEngine;
using System.Collections;

abstract public class UpgradeableObject : SavedStat 
{
	protected int upgrade_cost;
  [SerializeField]
	protected int max_upgrades = 0;

	abstract public void Upgrade();
	abstract public void RecalculateUpgradeCost();

  new void Awake()
	{
    Load();
		RecalculateUpgradeCost();
	}

	void OnDestroy()
	{
		Save ();
	}

  public virtual void Reset()
  {
    base.Reset();
  }

	public int GetUpgrades()
	{
		return GetValue();
	}

	public int GetUpgradeCost()
	{
		return upgrade_cost;
	}

	public bool PlayerHasEnoughMoney()
	{
    return (PlayerStats.Instance().GetCurrentMoney() >= upgrade_cost);
	}

  public int GetUpgradesOnReset()
  {
    return GetDefaultValueOnReset();
  }

  public void SetMaxUpgrades(int max_upgrades)
  {
    this.max_upgrades = max_upgrades;
  }
}
