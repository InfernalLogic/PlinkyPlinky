using UnityEngine;
using System.Collections;

abstract public class ScoringObject : UpgradeableObject 
{
	abstract public int GetPointValue();
  abstract protected void RecalculatePointValue();

  protected int point_value;

  new void Awake()
  {
    Load();
    RecalculateUpgradeCost();
    RecalculatePointValue();
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
			PlayerStats.Instance().SpendMoney(upgrade_cost);
      RecalculatePointValue();
			RecalculateUpgradeCost();
		}
		else
		{
			Debug.Log("Not enough moneys!");
		}
	}
}
