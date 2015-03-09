using UnityEngine;
using System.Collections;

public class CoinUpgrader : ScoringObjectUpgrade 
{
  [SerializeField]
  private PlinkagonUpgrade coin_critical_upgrader;

	public override void RecalculateUpgradeCost()
	{
    upgrade_cost = (ulong)((Mathf.Pow((float)(value + 1), 1.3f) * 50)); 
	}

  public override void RecalculatePointValue()
  {
    point_value = (value * 5) + 10;
  }

  public override int GetNextPointValue()
  {
    return point_value + 5;
  }
}

