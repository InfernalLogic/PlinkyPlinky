using UnityEngine;
using System.Collections;

public class BumperUpgrader : ScoringObject 
{
	public override void RecalculateUpgradeCost()
	{
    upgrade_cost = (int)((Mathf.Pow((float)(value + 1), 1.2f) * 150));
	}

	public override int GetPointValue()
	{
    return point_value;
	}

  protected override void RecalculatePointValue()
  {
    point_value = value;
  }
}
