using UnityEngine;
using System.Collections;

abstract public class UpgradeableObject : PlinkyObject 
{
  [SerializeField]
  private int upgrades_on_reset = 0;

	protected int upgrades;
	protected int upgrade_cost;
	protected int max_upgrades = 0;
	protected string upgrade_id;

	abstract public void Upgrade();
	abstract public void RecalculateUpgradeCost();
	abstract public void Reset();
	abstract protected void Initialize();

	void Start()
	{
		Initialize();
		Load ();
		RecalculateUpgradeCost();
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

	public bool PlayerHasEnoughMoney()
	{
    return (engine.player_stats.GetCurrentMoney() >= upgrade_cost);
	}

	public void Save()
	{
		if (!IsClone())
		{
			PlayerPrefs.SetInt(upgrade_id +"_upgrades", upgrades);
			Debug.Log (upgrade_id +" saved as: " + PlayerPrefs.GetInt(upgrade_id +"_upgrades", 0));
		}
	}

	public virtual void Load()
	{
		if (!IsClone ())
		{
      upgrades = PlayerPrefs.GetInt(upgrade_id + "_upgrades", upgrades_on_reset);
			Debug.Log (upgrade_id + " initialized to: " + upgrades);
		}
	}

	private bool IsClone()
	{
		return upgrade_id.Contains("(Clone)");
	}

  public int GetUpgradesOnReset()
  {
    return upgrades_on_reset;
  }
}
