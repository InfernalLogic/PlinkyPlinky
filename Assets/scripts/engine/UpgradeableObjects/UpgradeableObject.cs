using UnityEngine;
using System.Collections;

abstract public class UpgradeableObject : SavedStat 
{
  [SerializeField]
  protected int max_upgrades = 0;
	protected int upgrade_cost;

	abstract public void Upgrade();
	abstract public void RecalculateUpgradeCost();

  void Awake()
	{
    Load();
	}

  void Start()
  {
    RecalculateUpgradeCost();
  }

	void OnDestroy()
	{
		Save ();
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

  public bool UpgradesNotMaxedOut()
  {
    return value < (max_upgrades) || max_upgrades == 0;
  }
}
