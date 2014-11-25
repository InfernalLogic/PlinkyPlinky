using UnityEngine;
using System.Collections;

public class BumperUpgrader : ScoringObjectUpgrade 
{
	public override void RecalculateUpgradeCost()
	{
    upgrade_cost = (int)((Mathf.Pow((float)(value + 1), 1.4f) * 150));
	}

  public override void RecalculatePointValue()
  {
    point_value = value * 2;
  }
}
