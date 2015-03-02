using UnityEngine;
using System.Collections;

abstract public class ScoringObjectUpgrade : UpgradeableObject 
{
  [SerializeField]
  private string relevant_event_name;

  protected int point_value;

  abstract public void RecalculatePointValue();

  new protected virtual void OnEnable()
  {
    Events.ResetEvents += OnReset;
  }

  new protected virtual void OnDisable()
  {
    Events.ResetEvents -= OnReset;
  }

  new void Awake()
  {
    base.Awake();
    RecalculateUpgradeCost();
    RecalculatePointValue();
  }

  new protected virtual void OnReset(ResetType type)
  {
    base.OnReset(type);
    RecalculatePointValue();
  }

	public override void Upgrade()
	{
		if (PlayerHasEnoughCurrency())
		{
      ++value;
			MoneyTracker.Instance.SpendMoney(upgrade_cost);
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

  public virtual int GetPointValue()
  {
    return point_value;
  }

  public virtual int GetNextPointValue()
  {
    return point_value + 1;
  }
}
