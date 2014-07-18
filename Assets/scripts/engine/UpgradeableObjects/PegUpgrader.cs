using UnityEngine;
using System.Collections;

public class PegUpgrader : ScoringObject 
{
	public override void RecalculateUpgradeCost()
	{
		upgrade_cost = (int)((Mathf.Pow((float)(value + 1), 1.2f) * 200));
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
