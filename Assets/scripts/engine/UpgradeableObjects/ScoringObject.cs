using UnityEngine;
using System.Collections;

abstract public class ScoringObject : UpgradeableObject 
{
  [SerializeField]
  private string relevant_event_name;

  protected int point_value;

	abstract public int GetPointValue();
  abstract public void RecalculatePointValue();

  new void Awake()
  {
    Load();
    RecalculateUpgradeCost();
    RecalculatePointValue();
  }

	new public virtual void Reset()
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

  public string GetRelevantEventName()
  {
    return relevant_event_name;
  }
}
